using Data.Entities;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Controller
{
   public class GenreController
    {
        private readonly LibraryDbContext context = new LibraryDbContext();
        public GenreController()
        {

        }
        public GenreController(LibraryDbContext c)
        {
            this.context = c;
        }
        public async Task<List<Genre>> GetAllAsync()
        {
            return await context.Genres.ToListAsync();
        }
        public async Task AddAsync(string name)
        {
            context.Genres.Add(new Genre { Name = name });
            await context.SaveChangesAsync();
        }
        public async Task<List<Genre>> GetByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return await GetAllAsync();
            return await context.Genres.Where(g => g.Name==name).ToListAsync();
        }
        public async Task<Genre> GetByNameID(string name)
        {
            return await context.Genres.FirstOrDefaultAsync(g => g.Name == name);
        }
    }
}

