using MoviesApi.Models;

namespace MoviesApi.Repository
{
    public interface IGenreRepository
    {
        Task<int> AddAsync(Genre genre);
        Task<int> DeleteAsync(int id);
        Task<List<Genre>> GetAllAsync();
        Task<Genre> GetByIdAsync(int id);
        Task<bool> AnyValidAsync(int id);
        Task<Genre> GetByNameAsync(string name);
        Task<int> UpdateAsync(int id, CreateGenreDTO newGenre);
    }
}