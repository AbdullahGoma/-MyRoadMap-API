using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Repository;
using System.Xml.Linq;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreRepository genreRepo;

        public GenresController(IGenreRepository _genreRepo)
        {
            genreRepo = _genreRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await genreRepo.GetAllAsync());
        }

        [HttpGet("{id:int}", Name = "GetOneGenreRoute")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await genreRepo.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]CreateGenreDTO genre)
        {
            if (ModelState.IsValid)
            {
                var gen = new Genre { Name = genre.Name };
                await genreRepo.AddAsync(gen);
                string url = Url.Link("GetOneGenreRoute", new { id = gen.ID });
                return Created(url, gen);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync([FromRoute]int id, [FromBody]CreateGenreDTO genre)
        {
            if (ModelState.IsValid)
            {
                if (await genreRepo.UpdateAsync(id, genre) != 0)
                    return StatusCode(204, genre);
                else
                    return BadRequest("ID Not Valid: " + id);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            if(await genreRepo.DeleteAsync(id) != 0) 
                return Ok("Removed");
            return BadRequest("ID Not Valid: " + id);
        }


    }
}
