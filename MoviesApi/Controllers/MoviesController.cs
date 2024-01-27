using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Models;
using MoviesApi.Repository;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MoviesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMovieRepository movieRepo;
        private readonly IGenreRepository genreRepo;
        private new List<string> allowdExtentions = new List<string> { ".jpg", ".png" };
        private long maxAllowedPosterSize = 1048576;

        public MoviesController(IMovieRepository _movieRepo, IGenreRepository _genreRepo, IMapper mapper)
        {
            movieRepo = _movieRepo;
            genreRepo = _genreRepo;
            this.mapper = mapper;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllAsync()
        //{
        //    return Ok(await movieRepo.GetAllAsync());
        //}

        //[HttpGet("{DTO}")]
        //public async Task<IActionResult> GetAllDTOAsync()
        //{
        //    return Ok(await movieRepo.GetAllDTOAsync());
        //}


        //[HttpGet("Genre")]
        //public async Task<IActionResult> GetByGenreIdAsync(byte genreId)
        //{
        //    var movies = await movieRepo.GetAllByGenreIdAsync(genreId);
        //    return Ok(movies);
        //}


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var movies = await movieRepo.GetAllAsync();
            var data = mapper.Map<IEnumerable<MovieDetailsDTO>>(movies);
            return Ok(data);
        }

        [HttpGet("GetByGenreId")]
        public async Task<IActionResult> GetByGenreIdAsync(byte genreId)
        {
            var movies = await movieRepo.GetAll(genreId);
            var data = mapper.Map<IEnumerable<MovieDetailsDTO>>(movies);
            return Ok(data);
        }



        [HttpGet("{id:int}", Name = "GetOneMovieRoute")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var movie = await movieRepo.GetByIdAsync(id);
            if (movie == null)
                return NotFound("ID Not Valid");
            return Ok(movie);
        }

        [HttpGet("DTO/{id:int}", Name = "GetOneMovieDTORoute")]
        public async Task<IActionResult> GetByIdDTOAsync(int id)
        {
            var movie = await movieRepo.GetByIdDTOAsync(id);
            if (movie == null)
                return NotFound("ID Not Valid");
            var mdto = mapper.Map<MovieDetailsDTO>(movie);
            return Ok(mdto);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm]MovieDTO movieDTO)
        {
            if (ModelState.IsValid)
            {
                if(!allowdExtentions.Contains(Path.GetExtension(movieDTO.Poster.FileName).ToLower()))
                    return BadRequest("Only .png and .jpg Extensions are allowed!");
                if (movieDTO.Poster.Length > maxAllowedPosterSize)
                    return BadRequest("Max allowed Size for Poster is 1MB!");

                var isValidGenre = await genreRepo.AnyValidAsync(movieDTO.GenreID);
                if (!isValidGenre)
                    return BadRequest("Invalid Genre ID!");

                using var dataStream = new MemoryStream();
                await movieDTO.Poster.CopyToAsync(dataStream);


                var movie = mapper.Map<Movie>(movieDTO);
                movie.Poster = dataStream.ToArray();


                await movieRepo.AddAsync(movie);
                string url = Url.Link("GetOneMovieRoute", new { id = movie.ID });
                return Created(url, movie);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromForm] MovieDTO movieDTO)
        {
            if (ModelState.IsValid)
            {
                var movie = await movieRepo.GetByIdAsync(id);
                var isValidGenre = await genreRepo.AnyValidAsync(movieDTO.GenreID);
                if (!isValidGenre)
                    return BadRequest("Invalid Genre ID!");

                if(movieDTO.Poster != null)
                {
                    if (!allowdExtentions.Contains(Path.GetExtension(movieDTO.Poster.FileName).ToLower()))
                        return BadRequest("Only .png and .jpg Extensions are allowed!");
                    if (movieDTO.Poster.Length > maxAllowedPosterSize)
                        return BadRequest("Max allowed Size for Poster is 1MB!");

                    using var dataStream = new MemoryStream();
                    await movieDTO.Poster.CopyToAsync(dataStream);
                    movie.Poster = dataStream.ToArray();
                }


                if (await movieRepo.UpdateAsync(id, movieDTO) != 0)
                    return StatusCode(204, movieDTO);
                else
                    return BadRequest("ID Not Valid: " + id);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            if (await movieRepo.DeleteAsync(id) != 0)
                return Ok("Removed");
            return BadRequest("ID Not Valid: " + id);
        }
    }
}
