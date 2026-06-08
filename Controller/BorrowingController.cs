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
    public class BorrowingController
    {
        private readonly LibraryDbContext context = new LibraryDbContext();
        public BorrowingController()
        {

        }
        public BorrowingController(LibraryDbContext c)
        {
            this.context = c;
        }
        public async Task<List<Borrowing>> GetActiveBorrowings()
        {
            return await context.Borrowings.Include(b => b.Reader).Include(b => b.Book).Where(b => b.ReturnedDate == null).ToListAsync();
        }

            public async Task<bool> BorrowBook(int readerId, string rawBookId)
            {

                if (string.IsNullOrWhiteSpace(rawBookId) || !int.TryParse(rawBookId, out int bookId))
                {
                    throw new Exception("Моля, въведете валидно цифрово ID на книгата");
                }

                if (bookId <= 0 || readerId <= 0)
                {
                    throw new Exception("Невалидно ID на книга или читател");
                }

                var book = await context.Books.FindAsync(bookId);
                if (book == null)
                {
                    throw new Exception("Избраната книга не съществува ");
                }

                if (book.AvailableCopies <= 0)
                {
                    throw new Exception($"Книгата '{book.Title}' е изчерпана");
                }

              
                book.AvailableCopies--;
                context.Borrowings.Add(new Borrowing
                {
                    ReaderId = readerId,
                    BookId = bookId,
                    BorrowedDate = DateTime.Now
                });

                await context.SaveChangesAsync();
                return true;

            }
        public async Task<string> ReturnBook(int borrowingId)
        {
            if (borrowingId <= 0)
            {
                return " Невалидно ID ";
            }
            var borrowing = await context.Borrowings
                .Include(b => b.Book)
                .FirstOrDefaultAsync(b => b.Id == borrowingId);        
            if (borrowing == null)
            {
                return " Записът за заемане не беше намерен";
            }         
            if (borrowing.ReturnedDate != null)
            {
                return " Тази книга вече е отбелязана като върната";
            }         
            if (borrowing.Book != null)
            {
                borrowing.Book.AvailableCopies++;
            }

            await context.SaveChangesAsync();
            return "Книгата беше върната успешно";
        }
        public async Task<List<Borrowing>> GetByReaderName(string fName,string lname)
        {
            if (string.IsNullOrWhiteSpace(fName)|| string.IsNullOrWhiteSpace(lname)) return await GetActiveBorrowings();
            return await context.Borrowings
                .Include(b => b.Reader).Include(b => b.Book)
                .Where(b => b.ReturnedDate == null && (b.Reader.FirstName==fName || b.Reader.LastName==lname))
                .ToListAsync();
        }
        public async Task<string> ReturnBookByTitle(string bookTitle, int readerId)
        {
            if (string.IsNullOrWhiteSpace(bookTitle))
            {
                return "Моля, въведете заглавие на книга";
            }
            string cleanTitle = bookTitle; 
            bool bookExistsInLibrary = await context.Books
                .AnyAsync(b => b.Title == cleanTitle);
            if (!bookExistsInLibrary)
            {
                return "В библиотеката няма книга с такова заглавие";
            }
            var borrowing = await context.Borrowings
                .Include(b => b.Book)
                .FirstOrDefaultAsync(b => b.ReaderId == readerId &&
                                          b.Book.Title == cleanTitle &&
                                         b.ReturnedDate == null);
            if (borrowing == null)
            {
                return "Ти не си заел тази книга (или вече си я върнал)";
            }
  
            if (borrowing.Book != null)
            {
                borrowing.Book.AvailableCopies++;
            }
            borrowing.ReturnedDate = DateTime.Now;
            await context.SaveChangesAsync();
            return "Книгата беше върната успешно";
        }
    }
}

