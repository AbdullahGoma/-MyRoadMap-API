using FirstDemo.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstDemo.Controllers
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
        public async Task<IActionResult> GetAllEmployees()
        {
            return Ok(employeeRepo.GetAll());
        }

        [HttpGet("{id:int}", Name = "OneEmployeeRoute")]
        public IActionResult GetByID(int id)
        {
            return Ok(employeeRepo.GetByIdDTO(id));
        }

    }
}
