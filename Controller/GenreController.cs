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
        private readonly LibraryDbContext _context = new LibraryDbContext();
        public async Task<List<Genre>> GetAllAsync()
        {
            return await _context.Genres.ToListAsync();
        }
        public async Task AddAsync(string name)
        {
            _context.Genres.Add(new Genre { Name = name });
            await _context.SaveChangesAsync();
        }
        public async Task<List<Genre>> GetByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return await GetAllAsync();
            return await _context.Genres.Where(g => g.Name==name).ToListAsync();
        }
        public async Task<Genre> GetByNameID(string name)
        {
            return await _context.Genres.FirstOrDefaultAsync(g => g.Name == name);
        }
    }
}

