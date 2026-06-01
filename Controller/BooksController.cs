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
        LibraryDbContext context = new LibraryDbContext();
        public BooksController()
        {

        }
        public BooksController(LibraryDbContext c)
        {
            this.context = c;
        }
        public async Task<List<Books>> GetAllAsync()
        {
            return await context.Books.Include(b => b.Author).Include(b => b.Genre).ToListAsync();
        }
        public async Task<List<Books>> GetAvailableAsync()
        {
           return await context.Books.Include(b => b.Author).Where(b => b.AvailableCopies > 0).ToListAsync();
        }
        public async Task AddAsync(string title, int authorId, int genreId, int copies)
        {
            context.Books.Add(new Books { Title = title, AuthorId = authorId, GenreId = genreId, AvailableCopies = copies });
            await context.SaveChangesAsync();
        }
        public async Task<List<Books>> GetByTitleAsync(string title)
        {
            if (string.IsNullOrWhiteSpace(title)) return new List<Books>();
            return await context.Books
                .Include(b => b.Author).Include(b => b.Genre)
                .Where(b => b.Title==title).ToListAsync();
        }
        public async Task<bool> DeleteByIdAsync(int id)
        {   
            var book = await context.Books.FindAsync(id);        
            if (book == null) return false;
            context.Books.Remove(book);
            await context.SaveChangesAsync();
            return true; 
        }
    }
}

