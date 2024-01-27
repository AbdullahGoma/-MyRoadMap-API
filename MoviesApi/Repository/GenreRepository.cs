using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;
using MoviesApi.Models;

namespace MoviesApi.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly AppDbContext context;

        public GenreRepository(AppDbContext _context)
        {
            context = _context;
        }

        public async Task<List<Genre>> GetAllAsync()
        {
            return await context.Genre.OrderBy(d => d.Name).ToListAsync();
        }

        public async Task<Genre> GetByIdAsync(int id)
        {
            return await context.Genre.FirstOrDefaultAsync(g => g.ID == id);
        }

        
        public async Task<bool> AnyValidAsync(int id)
        {
            return await context.Genre.AnyAsync(g => g.ID == id);
        }


        public async Task<Genre> GetByNameAsync(string name)
        {
            return await context.Genre.FirstOrDefaultAsync(g => g.Name == name);
        }

        public async Task<int> AddAsync(Genre genre)
        {
            await context.Genre.AddAsync(genre);
            return await context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(int id, CreateGenreDTO newGenre)
        {
            Genre genre = await context.Genre.FirstOrDefaultAsync(n => n.ID == id);
            if(genre != null)
            {
                genre.Name = newGenre.Name;
                return await context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> DeleteAsync(int id)
        {
            Genre genre = await context.Genre.FirstOrDefaultAsync(n => n.ID == id);
            if (genre != null)
            {
                context.Genre.Remove(genre);
                return await context.SaveChangesAsync();
            }
            return 0;
        }


    }
}
