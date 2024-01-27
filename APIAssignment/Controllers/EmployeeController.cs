using APIAssignment.Models;
using APIAssignment.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepo;

        public EmployeeController(IEmployeeRepository _employeeRepo)
        {
            employeeRepo = _employeeRepo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(employeeRepo.GetAll()); // 200
        }

        //[HttpGet("{id:int}", Name = "GetOneEmployeeRoute")]
        //public IActionResult GetByID(int id)
        //{
        //    return Ok(employeeRepo.GetByID(id));
        //}

        [HttpGet("{Name:alpha}")]
        public IActionResult GetByName(string Name)
        {
            return Ok(employeeRepo.GetByName(Name));
        }


        [HttpGet("{id:int}", Name = "OneEmployeeRoute")]
        public IActionResult GetByIDDTO(int id)
        {
            return Ok(employeeRepo.GetByIdDTO(id));
        }



        [HttpPost]
        public IActionResult PostAllEmployee(Employee employee)
        {
            if(ModelState.IsValid)
            {
                employeeRepo.Create(employee);
                string url = Url.Link("GetOneEmployeeRoute", new { id = employee.ID });
                return Created(url, employee);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] Employee employee)
        {
            if (ModelState.IsValid)
            {
                if(employeeRepo.Update(id, employee) != 0)
                    return StatusCode(204, employee);
                else
                    return BadRequest("ID Not Valid");
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Remove(int id)
        {
            if (employeeRepo.Delete(id) == 0)
            {
                return BadRequest("ID Not Found");
            }
            else if (employeeRepo.Delete(id) == -1)
            {
                return BadRequest("Record Not Removed From Database");
            }
            return StatusCode(204, "Record Removed Success");
        }

    }
}
