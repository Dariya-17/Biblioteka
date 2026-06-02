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
    public class GenreControllerTest
    {
        [Test]
        public async Task Test_GetAll()
        {

            var context = TestDb.CreateContext();
            context.Genres.Add(new Genre { Name = "Фентъзи" });
            context.Genres.Add(new Genre { Name = "Ужаси" });
            await context.SaveChangesAsync();
            GenreController controller = new GenreController(context);
            List<Genre> result = await controller.GetAll();
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Фентъзи", result[0].Name);
            Assert.AreEqual("Ужаси", result[1].Name);
        }
        [Test]
        public async Task Test_Add()
        {
            var context = TestDb.CreateContext();
            GenreController controller = new GenreController(context);
            string genreName = " фантастика";
            await controller.Add(genreName);     
            var genreInDb = await context.Genres.FirstOrDefaultAsync(g => g.Name == genreName);
            Assert.IsNotNull(genreInDb);
            Assert.AreEqual(genreName, genreInDb.Name);
            Assert.IsTrue(genreInDb.Id > 0);
        }
        [Test]
        public async Task Test_GetByName()
        {     
           var context = TestDb.CreateContext();
            context.Genres.Add(new Genre { Name = "Трилър" });
            context.Genres.Add(new Genre { Name = "Драма" });
            await context.SaveChangesAsync();
           GenreController controller = new GenreController(context);           
            List<Genre> result = await controller.GetByName("Трилър");          
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Трилър", result[0].Name);
        }
        [Test]
        public async Task Test_GetByNameID_()
        {
          
            var context = TestDb.CreateContext();
            var genre = new Genre { Name = "Поезия" };
            context.Genres.Add(genre);
            await context.SaveChangesAsync();
            GenreController controller = new GenreController(context);   
            Genre result = await controller.GetByNameID("Поезия");        
            Assert.IsNotNull(result);
        }
        [Test]
        public async Task Test_GetByNameID_False()
        {       
            var context = TestDb.CreateContext();
            GenreController controller = new GenreController(context);        
            Genre result = await controller.GetByNameID("ne");       
            Assert.IsNull(result);
        }
    }
   }
