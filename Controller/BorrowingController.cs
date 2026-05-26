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
        private readonly LibraryDbContext _context = new LibraryDbContext();
        public async Task<List<Borrowing>> GetActiveBorrowingsAsync()
        {
           return await _context.Borrowings.Include(b => b.Reader).Include(b => b.Book).Where(b => b.ReturnedDate == null).ToListAsync();
        }
        public async Task<bool> BorrowBookAsync(int readerId, int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null || book.AvailableCopies <= 0)
                return false;
            book.AvailableCopies--;  
            _context.Borrowings.Add(new Borrowing
            {
                ReaderId = readerId,
                BookId = bookId,
                BorrowedDate = DateTime.Now 
            });
           await _context.SaveChangesAsync();
            return true;
        }
        public async Task ReturnBookAsync(int borrowingId)
        {
            var borrowing = await _context.Borrowings.Include(b => b.Book).FirstOrDefaultAsync(b => b.Id == borrowingId);
            if (borrowing != null)
            {
                borrowing.ReturnedDate = DateTime.Parse(Console.ReadLine());
                borrowing.Book.AvailableCopies++;
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<Borrowing>> GetByReaderNameAsync(string fName,string lname)
        {
            if (string.IsNullOrWhiteSpace(fName)|| string.IsNullOrWhiteSpace(lname)) return await GetActiveBorrowingsAsync();
            return await _context.Borrowings
                .Include(b => b.Reader).Include(b => b.Book)
                .Where(b => b.ReturnedDate == null && (b.Reader.FirstName==fName || b.Reader.LastName==lname))
                .ToListAsync();
        }
    }
}

