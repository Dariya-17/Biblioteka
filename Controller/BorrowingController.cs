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
        public async Task<bool> BorrowBook(int readerId, int bookId)
        {
            var book = await context.Books.FindAsync(bookId);
            if (book == null || book.AvailableCopies <= 0)
                return false;
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
    }
}

