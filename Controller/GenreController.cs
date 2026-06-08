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
        public async Task<List<Genre>> GetAll()
        {
            return await context.Genres.ToListAsync();
        }
        public async Task<string> Add(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Невалидно име на жанр ");
            }

            name = name.Trim();
            bool genreExists = await IsGenreExisting(name);
            if (genreExists)
            {
                throw new Exception($"Този жанр {name} вече съществува");
            }
            context.Genres.Add(new Genre { Name = name });
            await context.SaveChangesAsync();

            return "Успешно добавяне на жанр";
        }
        public async Task<List<Genre>> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new List<Genre>();
            return await context.Genres.Where(g => g.Name.Trim() == name.Trim()).ToListAsync();
        }
        public async Task<Genre> GetByNameID(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            return await context.Genres
                .FirstOrDefaultAsync(g => g.Name.Trim() == name.Trim());
        }
        public async Task<bool> IsGenreExisting(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            return await context.Genres
                .AnyAsync(g => g.Name.Trim() == name.Trim());
        }
    }
}


