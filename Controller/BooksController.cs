using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class BooksController
    {
        LibraryDbContext _context = new LibraryDbContext();
        public async Task<List<Books>> GetAllAsync()
        {
            return await _context.Books.Include(b => b.Author).Include(b => b.Genre).ToListAsync();
        }
        public async Task<List<Books>> GetAvailableAsync()
        {
           return await _context.Books.Include(b => b.Author).Where(b => b.AvailableCopies > 0).ToListAsync();
        }
        public async Task AddAsync(string title, int authorId, int genreId, int copies)
        {
            _context.Books.Add(new Books { Title = title, AuthorId = authorId, GenreId = genreId, AvailableCopies = copies });
            await _context.SaveChangesAsync();
        }
        public async Task<List<Books>> GetByTitleAsync(string title)
        {
            if (string.IsNullOrWhiteSpace(title)) return await GetAllAsync();
            return await _context.Books
                .Include(b => b.Author).Include(b => b.Genre)
                .Where(b => b.Title==title).ToListAsync();
        }
    }
}

