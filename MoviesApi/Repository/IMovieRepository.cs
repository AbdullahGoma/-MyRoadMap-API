namespace MoviesApi.Repository
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAll(byte genreId = 0);
        Task<int> AddAsync(Movie movie);
        Task<int> DeleteAsync(int id);
        Task<List<Movie>> GetAllAsync();
        Task<List<MovieDetailsDTO>> GetAllDTOAsync();
        Task<List<MovieDetailsDTO>> GetAllByGenreIdAsync(byte genreId);
        Task<Movie> GetByIdAsync(int id);
        Task<Movie> GetByIdDTOAsync(int id);
        Task<Movie> GetByNameAsync(string title);
        Task<int> UpdateAsync(int id, MovieDTO movieDTO);
    }
}