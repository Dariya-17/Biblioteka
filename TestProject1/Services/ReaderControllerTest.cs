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
   public class ReaderControllerTest
    {
        [Test]
        public async Task Test_GetAll()
        {
           
            var context = TestDb.CreateContext();
            context.Readers.Add(new Reader { FirstName = "Петър", LastName = "Петров", Email = "pesho@abv.bg", PhoneNumber = "0888111222" });
            context.Readers.Add(new Reader { FirstName = "Мария", LastName = "Георгиева", Email = "mary@abv.bg", PhoneNumber = "0888333444" });
            await context.SaveChangesAsync();
            ReaderController controller = new ReaderController(context);      
            List<Reader> result = await controller.GetAllAsync();     
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Петър", result[0].FirstName);
            Assert.AreEqual("Мария", result[1].FirstName);
        }
        [Test]
        public async Task Test_Add()
        {

            var context = TestDb.CreateContext();
            ReaderController controller = new ReaderController(context);
            string fName = "Иван";
            string lName = "Иванов";
            string email = "ivan@abv.bg";
            string phone = "0899123456";
            string username = "admin99";
            string password = "password";        
            await controller.AddAsync(fName, lName, email, phone, username, password);        
            var reader = await context.Readers.FirstOrDefaultAsync(r => r.Email == email);
            Assert.IsNotNull(reader);
            Assert.AreEqual(fName, reader.FirstName);
            Assert.AreEqual(lName, reader.LastName);
            Assert.AreEqual(phone, reader.PhoneNumber);            
            var user = await context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            Assert.IsNotNull(user);
            Assert.AreEqual(password, user.Password);
        }
     [Test]
public async Task Test_GetByName()
{
    var context = TestDb.CreateContext();
    context.Readers.Add(new Reader { FirstName = "Георги", LastName = "Димитров",Email="ГД094773",PhoneNumber="084647378" });
    await context.SaveChangesAsync();
    
    ReaderController controller = new ReaderController(context);        
    List<Reader> result = await controller.GetByNameAsync("Георги", "Димитров");         
    Assert.IsNotNull(result);
    Assert.AreEqual(1, result.Count);
    Assert.AreEqual("Георги", result[0].FirstName);
    Assert.AreEqual("Димитров", result[0].LastName); 
}
    }
}
