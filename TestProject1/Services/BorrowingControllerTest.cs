using Controller;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject1.Helpers;

namespace TestProject1.Services
{
   public class BorrowingControllerTest
    {
        [Test]
        public async Task Test_GetActiveBorrowing()
        {  
            var context = TestDb.CreateContext();      
            var reader = new Reader
            {
                FirstName = "Иван",
                LastName = "Иванов",
                Email = "ivan.com",        
                PhoneNumber = "0888111222"      
            };
            var book = new Books { Title = "Под игото", AvailableCopies = 5 };
            context.Readers.Add(reader);
            context.Books.Add(book);
            await context.SaveChangesAsync();
            context.Borrowings.Add(new Borrowing
            {
                Reader = reader,
                Book = book,
                BorrowedDate = DateTime.Now,
                ReturnedDate = null
            });
            await context.SaveChangesAsync();       
            BorrowingController controller = new BorrowingController(context);       
            List<Borrowing> result = await controller.GetActiveBorrowings();        
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count );
            Assert.IsNull(result[0].ReturnedDate);
        }
        [Test]
        public async Task Test_BorrowBook()
        {
            var context = TestDb.CreateContext();
            var reader = new Reader
            {
                FirstName = "Петър",
                LastName = "Петров",
                Email = "pesho.com",
                PhoneNumber = "0888333444"
            };
            var book = new Books { Title = "Бай Ганьо", AvailableCopies = 3 };
            context.Readers.Add(reader);
            context.Books.Add(book);
            await context.SaveChangesAsync();
            BorrowingController controller = new BorrowingController(context);
            bool isSuccess = await controller.BorrowBook(reader.Id, book.Id);         
            Assert.IsTrue(isSuccess);         
            var updatedBook = await context.Books.FindAsync(book.Id);
            Assert.AreEqual(2, updatedBook.AvailableCopies);        
            var borrowing = await context.Borrowings.FirstOrDefaultAsync(b => b.ReaderId == reader.Id && b.BookId == book.Id);
            Assert.IsNotNull(borrowing);
            Assert.IsNull(borrowing.ReturnedDate);
        }
        [Test]
        public async Task Test_BorrowBook_False_()
        {
            var context = TestDb.CreateContext();
           var reader = new Reader
            {
                FirstName = "Мария",
                LastName = "Георгиева",
                Email = "mariya.com",        
                PhoneNumber = "0888555666"     
            };
            var book = new Books { Title = "nz", AvailableCopies = 0 };
            context.Readers.Add(reader);
            context.Books.Add(book);
            await context.SaveChangesAsync();
            BorrowingController controller = new BorrowingController(context);
            bool isSuccess = await controller.BorrowBook(reader.Id, book.Id);     
            Assert.IsFalse(isSuccess); 
            int borrowingsCount = await context.Borrowings.CountAsync();
            Assert.AreEqual(0, borrowingsCount); 
        }
        [Test]
        public async Task Test_GetByReaderName_Empty()
        {
            var context = TestDb.CreateContext();
            var reader = new Reader { FirstName = "Елена", LastName = "Петрова", Email = "elena@test.com", PhoneNumber = "0883" };
            var book = new Books { Title = "Елисавета" };
            context.Readers.Add(reader);
            context.Books.Add(book);
            await context.SaveChangesAsync();
            context.Borrowings.Add(new Borrowing { Reader = reader, Book = book, BorrowedDate = DateTime.Now, ReturnedDate = null });
            await context.SaveChangesAsync();
            BorrowingController controller = new BorrowingController(context);
            List<Borrowing> result = await controller.GetByReaderName("", "   ");
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }
        [Test]
        public async Task Test_GetByReaderName()
        {
            var context = TestDb.CreateContext();
            var reader1 = new Reader { FirstName = "Георги", LastName = "Димитров", Email = "g1@test.com", PhoneNumber = "0881" };
            var reader2 = new Reader { FirstName = "Симеон", LastName = "Борисов", Email = "s2@test.com", PhoneNumber = "0882" };
            var book = new Books { Title = "Време разделно" };
            context.Readers.AddRange(reader1, reader2);
            context.Books.Add(book);
            await context.SaveChangesAsync();      
            context.Borrowings.Add(new Borrowing { Reader = reader1, Book = book, BorrowedDate = DateTime.Now, ReturnedDate = null });          
            context.Borrowings.Add(new Borrowing { Reader = reader2, Book = book, BorrowedDate = DateTime.Now, ReturnedDate = null });
            await context.SaveChangesAsync();
            BorrowingController controller = new BorrowingController(context);        
            List<Borrowing> result = await controller.GetByReaderName("Георги", "Димитров");     
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(reader1.Id, result[0].ReaderId);
        }
 
        

    }
}
