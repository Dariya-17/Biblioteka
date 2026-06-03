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
   public  class BooksControllerTest
    {
        [Test]
        public async Task Test_GetAvailable()
        {
            var context = TestDb.CreateContext();
            var author = new Author { FirstName = "Алеко", LastName = "Константинов" };
            context.Authors.Add(author);
            await context.SaveChangesAsync();
            context.Books.Add(new Books { Title = "Бай Ганьо", AuthorId = author.Id, AvailableCopies = 3 });
            context.Books.Add(new Books { Title = "До Чикаго и назад", AuthorId = author.Id, AvailableCopies = 0 });
            await context.SaveChangesAsync();
            BooksController controller = new BooksController(context);
            List<Books> result = await controller.GetAvailable();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count); 
            Assert.AreEqual("Бай Ганьо", result[0].Title);
        }
     
        [Test]
        public async Task Test_GetAll_()
        {
            var context = TestDb.CreateContext();
            var author = new Author { FirstName = "Иван", LastName = "Вазов" };
            var genre = new Genre { Name = "Роман" };
            context.Authors.Add(author);
            context.Genres.Add(genre);
            await context.SaveChangesAsync();
            context.Books.Add(new Books { Title = "Под игото", AuthorId = author.Id, GenreId = genre.Id, AvailableCopies = 5 });
            context.Books.Add(new Books { Title = "Нова", AuthorId = author.Id, GenreId = genre.Id, AvailableCopies = 2 });
            await context.SaveChangesAsync();
            BooksController controller = new BooksController(context);
            List<Books> result = await controller.GetAll();
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.IsNotNull(result[0].Author); 
            Assert.IsNotNull(result[0].Genre);  
        }
        [Test]
        public async Task Test_Add_()
        {
            var context = TestDb.CreateContext();
            BooksController controller = new BooksController(context);
            await controller.AddAsync("Тютюн", 1, 1, 10);
            var book = await context.Books.FirstOrDefaultAsync(b => b.Title == "Тютюн");
            Assert.IsNotNull(book);
            Assert.AreEqual(1, book.AuthorId);
            Assert.AreEqual(1, book.GenreId);
            Assert.AreEqual(10, book.AvailableCopies);
        }
        [Test]
        public async Task Test_DeleteById_ReturnTrue()
        {
            var context = TestDb.CreateContext();
            var book = new Books { Title = "nz", AvailableCopies = 1 };
            context.Books.Add(book);
            await context.SaveChangesAsync();
           BooksController controller = new BooksController(context);
            bool isDeleted = await controller.DeleteById(book.Id);        
            Assert.IsTrue(isDeleted);         
            var bookInDb = await context.Books.FindAsync(book.Id);
            Assert.IsNull(bookInDb);
        }
        [Test]
        public async Task Test_DeleteById_ReturnFalse()
        {
            var context = TestDb.CreateContext();
            BooksController controller = new BooksController(context);          
            bool isDeleted = await controller.DeleteById(999);        
            Assert.IsFalse(isDeleted);
        }

        [Test]
        public async Task Test_IsBookTitleTaken()
        {
        
            var context = TestDb.CreateContext();
            context.Books.Add(new Books { Title = "Под игото", AvailableCopies = 3 });
            await context.SaveChangesAsync();
            var controller = new BooksController(context);        
            bool resultTrue = await controller.IsBookTitleTaken("Под игото");
            bool resultFalse = await controller.IsBookTitleTaken("Някаква друга книга");
            Assert.IsTrue(resultTrue);
            Assert.IsFalse(resultFalse);
        }

 
        [Test]
        public async Task Test_GetByTitle_()
        {          
            var context = TestDb.CreateContext();
            var author = new Author { FirstName = "Иван", LastName = "Вазов" };
            var genre = new Genre { Name = "Роман" };
            context.Authors.Add(author);
            context.Genres.Add(genre);
            await context.SaveChangesAsync();
            context.Books.Add(new Books { Title = "Тютюн", AuthorId = author.Id, GenreId = genre.Id, AvailableCopies = 2 });
            await context.SaveChangesAsync();
            var controller = new BooksController(context);         
            List<Books> result = await controller.GetByTitle("Тютюн");         
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Тютюн", result[0].Title);
        }

       
        [Test]
        public async Task Test_DeleteById_()
        {
        
            var context = TestDb.CreateContext();
            var book = new Books { Title = "Бай Ганьо", AvailableCopies = 5 };
            context.Books.Add(book);
            await context.SaveChangesAsync();
            var controller = new BooksController(context);      
            bool isDeleted = await controller.DeleteById(book.Id);          
            Assert.IsTrue(isDeleted);        
            var bookInDb = await context.Books.FindAsync(book.Id);
            Assert.IsNull(bookInDb);
        }
    }
}
