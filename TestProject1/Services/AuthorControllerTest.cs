using Controller;
using Data;
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
   public class AuthorControllerTest
    {
        private LibraryDbContext context;
        private AuthorController controller;
        [Test]
        public async Task Test_AddAuthor()
        {     
            var context = TestDb.CreateContext();          
            AuthorController controller = new AuthorController(context);         
            string firstName = "Алеко";
            string lastName = "Константинов";         
            string message = await controller.AddAuthor(firstName, lastName);
            Assert.IsNotNull(message);       
            var authorInDb = await context.Authors
                .FirstOrDefaultAsync(a => a.FirstName == firstName && a.LastName == lastName);
            Assert.IsNotNull(authorInDb);
            Assert.AreEqual(firstName, authorInDb.FirstName);
            Assert.AreEqual(lastName, authorInDb.LastName);
           
        }
        [Test]
        public async Task Test_GetByName_()
        {          
            var context = TestDb.CreateContext();       
            var existingAuthor = new Author
            {
                FirstName = "Елин",
                LastName = "Пелин"
            };
            context.Authors.Add(existingAuthor);
            await context.SaveChangesAsync();           
            AuthorController controller = new AuthorController(context);           
            Author result = await controller.GetByNameAsync("Елин", "Пелин");                   
            Assert.AreEqual("Елин", result.FirstName);
            Assert.AreEqual("Пелин", result.LastName);
            Assert.AreEqual(existingAuthor.Id, result.Id); 
        }
        [Test]
        public async Task Test_GetAuthorById_()
        {        
            var context = TestDb.CreateContext();       
            var author = new Author
            {
                FirstName = "Христо",
                LastName = "Ботев"
            };
            context.Authors.Add(author);
            await context.SaveChangesAsync();
            AuthorController controller = new AuthorController(context);        
            Author result = await controller.GetAuthorById(author.Id);            
            Assert.AreEqual("Христо", result.FirstName);
            Assert.AreEqual("Ботев", result.LastName);
            Assert.AreEqual(author.Id, result.Id);
        }
        [Test]
        public async Task Test_GetAuthorById_False()
        {       
            var context = TestDb.CreateContext();           
            AuthorController controller = new AuthorController(context);
            Author result = await controller.GetAuthorById(999);
            Assert.IsNull(result);
        }
    }
}
