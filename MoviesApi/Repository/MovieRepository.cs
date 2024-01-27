using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;

namespace MoviesApi.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext context;

        public MovieRepository(AppDbContext _context)
        {
            context = _context;
        }

        public async Task<List<Movie>> GetAllAsync()
        {
            return await context.Movies.Include(g => g.Genre).OrderByDescending(d => d.Rate).ToListAsync();
        }

        public async Task<List<MovieDetailsDTO>> GetAllDTOAsync()
        {
            return await context.Movies.Include(g => g.Genre)
                .Select(m => new MovieDetailsDTO
                {
                    ID = m.ID,
                    GenreID = m.GenreID,
                    GenreName = m.Genre.Name,
                    Poster = m.Poster,
                    Rate = m.Rate,
                    Storyline = m.Storyline,
                    Title = m.Title,
                    Year = m.Year
                }).OrderByDescending(d => d.Rate).ToListAsync();
        }

        public async Task<List<MovieDetailsDTO>> GetAllByGenreIdAsync(byte genreId)
        {
            var movies = await context.Movies.Include(g => g.Genre)
                .Where(m => m.GenreID == genreId)
                .Select(m => new MovieDetailsDTO
                {
                    ID = m.ID,
                    GenreID = m.GenreID,
                    GenreName = m.Genre.Name,
                    Poster = m.Poster,
                    Rate = m.Rate,
                    Storyline = m.Storyline,
                    Title = m.Title,
                    Year = m.Year
                }).OrderByDescending(d => d.Rate).ToListAsync();
            return movies;
        }


        public async Task<IEnumerable<Movie>> GetAll(byte genreId = 0)
        {
            return await context.Movies
                .Where(m => m.GenreID == genreId || genreId == 0)
                .OrderByDescending(d => d.Rate)
                .Include(g => g.Genre)
                .ToListAsync();
        }



        public async Task<Movie> GetByIdAsync(int id) => await context.Movies.FirstOrDefaultAsync(g => g.ID == id);


        public async Task<Movie> GetByIdDTOAsync(int id) {
            return await context.Movies.Include(g => g.Genre)  
                                       .FirstOrDefaultAsync(g => g.ID == id); ;
        }


        public async Task<Movie> GetByNameAsync(string title)
        {
            return await context.Movies.FirstOrDefaultAsync(g => g.Title == title);
        }

        public async Task<int> AddAsync(Movie movie)
        {
            await context.Movies.AddAsync(movie);
            return await context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(int id, MovieDTO movieDTO)
        {
            Movie movie = await context.Movies.FirstOrDefaultAsync(n => n.ID == id);

            using var dataStream = new MemoryStream();
            await movieDTO.Poster.CopyToAsync(dataStream);

            if (movie != null)
            {
                movie.Title = movieDTO.Title;
                movie.Rate = movieDTO.Rate;
                movie.Storyline = movieDTO.Storyline;
                movie.Year = movieDTO.Year;
                movie.Poster = dataStream.ToArray();
                movie.GenreID = movieDTO.GenreID;
                return await context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> DeleteAsync(int id)
        {
            Movie movie = await context.Movies.FirstOrDefaultAsync(n => n.ID == id);
            if (movie != null)
            {
                context.Movies.Remove(movie);
                return await context.SaveChangesAsync();
            }
            return 0;
        }

    }
}
