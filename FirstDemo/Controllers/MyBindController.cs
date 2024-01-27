using FirstDemo.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // Call to runtime that not MVC but API, handle Binding
    public class MyBindController : ControllerBase
    {
        // Bind primitive type ==> Route Segment /id/ ==> Query String ?id=1
        // Bind Complex type ==> Request Body
        //[HttpGet("{id:int}")] // => Route Segment
        //// Route come before query string
        //public IActionResult Get1(int id, string name) 
        //{
        //    return Ok();
        //}

        [HttpPost]
        public IActionResult Add([FromBody]Department department, string name)// Department => Complex Type => Request Body
        {
            return Ok(department);
        }
        [HttpGet("{id:int}")]
        public IActionResult Get2(int id, [FromBody]string name)
        {
            return Ok();
        }

        [HttpGet("{Name:alpha}/{Manager:alpha}")]
        public IActionResult Get3([FromBody]int id, [FromRoute]Department department)
        {
            return Ok();
        }

    }
}
