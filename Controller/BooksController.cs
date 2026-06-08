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
        public async Task<List<Books>> GetAll()
        {
            return await context.Books.Include(b => b.Author).Include(b => b.Genre).ToListAsync();
        }
        public async Task<List<Books>> GetAvailable()
        {
            var availableBooks = await context.Books
          .Include(b => b.Author)
          .Include(b => b.Genre)
          .Where(b => b.AvailableCopies > 0)
          .ToListAsync();
            if (availableBooks == null || availableBooks.Count == 0)
            {
                throw new Exception("В момента няма свободни книги в библиотеката");
            }

            return availableBooks;
        }
        public async Task<string> AddAsync(string title, int authorId, int genreId, int copies)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return " Заглавието на книгата е задължително";
            }
            if (authorId <= 0 || genreId <= 0)
            {
                return " Изберете валиден автор и жанр";
            }
            if (copies < 0)
            {
                return " Броят копия не може да бъде отрицателно число";
            }       
            bool isTaken = await IsBookTitleTaken(title);
            if (isTaken)
            {
                return $" Книга със заглавие {title} вече съществува";
            }

            context.Books.Add(new Books { Title = title, AuthorId = authorId, GenreId = genreId, AvailableCopies = copies });
            await context.SaveChangesAsync();
            return "Книгата беше добавена успешно";
        }
        public async Task<List<Books>> GetByTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return new List<Books>();
            }

            return await context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Where(b => b.Title.Trim() == title.Trim())
                .ToListAsync();
        }
        public async Task<bool> DeleteById(int id)
        {
            if (id <= 0)
            {
                return false;
            }

            var book = await context.Books.FindAsync(id);
            if (book == null)
            {
                return false;
            }

            context.Books.Remove(book);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> IsBookTitleTaken(string title)
        {
            if(string.IsNullOrWhiteSpace(title))
        {
                return false;
            }

            return await context.Books.AnyAsync(b => b.Title == title);
        }
    }
}

