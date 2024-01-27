using APIAssignment.Models;
using APIAssignment.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository departmentRepo;

        public DepartmentController(IDepartmentRepository _departmentRepo)
        {
            departmentRepo = _departmentRepo;
        }
        //api/Department
        // Action
        [HttpGet]
        public IActionResult GetAllDepartment()
        {
            // Status Code 200
            return Ok(departmentRepo.GetAll()); // Response Body Json (Serialization)
        }

        [HttpGet("Employees/{id:int}")]
        //[Route("{id:int}")] //api/Department/id
        public IActionResult GetByIdDTO(int id)
        {
            return Ok(departmentRepo.GetByNameDTO(id));
        }

        [HttpGet("{id:int}", Name = "GetOneDepartmentRoute")]
        //[Route("{id:int}")] //api/Department/id
        public IActionResult GetByID(int id)
        {
            return Ok(departmentRepo.GetByID(id));
        }

        [HttpGet("{Name:alpha}")]
        //[Route("{Name:alpha}")] //api/Department/Name
        public IActionResult GetByName(string Name)
        {
            return Ok(departmentRepo.GetByName(Name));
        }


        //api/Department
        [HttpPost]
        public IActionResult PostAllDepartment(Department department)
        {
            if (ModelState.IsValid)
            {

                departmentRepo.Create(department);
                //return Ok("Saved");
                // How to get Current Domain?
                string url = Url.Link("GetOneDepartmentRoute", new { id = department.ID });
                return Created(url, department);
            }
            //return BadRequest("Department Not Valid");
            return BadRequest(ModelState);
        }

        //api/Department
        [HttpPut("{id:int}")]
        //[HttpPatch]
        public IActionResult Update([FromRoute] int id, [FromBody] Department department)
        {
            if (ModelState.IsValid)
            {
                if (departmentRepo.Update(id, department) != 0)
                    return StatusCode(204, department);
                else
                    return BadRequest("ID Not Valid");
                //if(department != null)
                //{
                //    departmentRepo.Update(id, department);
                //    return StatusCode(204, department);
                //}
                //return BadRequest("ID Not Valid");
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Remove(int id)
        {
            if (departmentRepo.Delete(id) == 0)
            {
                return BadRequest("ID Not Found");
            }
            else if (departmentRepo.Delete(id) == -1)
            {
                return BadRequest("Record Not Removed From Database");
            }
            return StatusCode(204, "Record Removed Success");
        }
    }
}
