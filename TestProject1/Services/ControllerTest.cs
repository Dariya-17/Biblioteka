using Controller;
using Data;
using Data.Entities;
using Data.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject1.Helpers;

namespace TestProject1.Services
{
    public class ControllerTest
    {
        [Test]
        public async Task Test_Login()
        {
            var context = TestDb.CreateContext();
            context.Users.Add(new User
            {
                UserName = "admin3",
                Password = "123",
                Role = RoleType.Admin
            });
            await context.SaveChangesAsync();
            RegisterLoginController controller = new RegisterLoginController(context);
            User? user = await controller.Login("admin3", "123");
            Assert.IsNotNull(user);
            Assert.AreEqual("admin3", user.UserName);
        }
        [Test]
        public async Task RegisterAdminATest()
        {
            var context = TestDb.CreateContext(); 
            context.Users.Add(new User
            {
                UserName = "admin67",
                Password = "123",
                Role = RoleType.Admin
            });
            await context.SaveChangesAsync();
            RegisterLoginController controller = new RegisterLoginController(context);
          string nz= await controller.RegisterAdmin("admin3", "123",RoleType.Admin);
            Assert.IsNotNull(nz);
            Assert.AreEqual("Успешна регистрация", nz );
        }
      
        [Test]
        public async Task Test_Register_User()
        {
           
            var context = TestDb.CreateContext();
            var controller = new Controller.RegisterLoginController(context);
            string username = "kaloian123";
            string password = "password123";
            string firstName = "Калоян";
            string lastName = "Иванов";
            string email = "kaloian@test.com";
            string phone = "0888999111";
            RoleType role = RoleType.Reader;         
            bool result = await controller.Register(username, password, firstName, lastName, email, phone, role);          
            Assert.IsTrue(result);        
            var userInDb = await context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            Assert.IsNotNull(userInDb);
            Assert.AreEqual(role, userInDb.Role);           
            var readerInDb = await context.Readers.FirstOrDefaultAsync(r => r.UserId == userInDb.Id);
            Assert.IsNotNull(readerInDb);
            Assert.AreEqual(firstName, readerInDb.FirstName);
            Assert.AreEqual(email, readerInDb.Email);
        }
      
        [Test]
        public async Task Test_Register_False()
        {
          
            var context = TestDb.CreateContext();
            context.Users.Add(new User { UserName = "existingUser", Password = "123", Role = RoleType.Reader });
            await context.SaveChangesAsync();
            var controller = new Controller.RegisterLoginController(context);          
            bool result = await controller.Register("existingUser", "newpass", "Петър", "Петров", "petar@test.com", "088", RoleType.Reader);          
            Assert.IsFalse(result);     
            var readerInDb = await context.Readers.FirstOrDefaultAsync(r => r.FirstName == "Петър");
            Assert.IsNull(readerInDb);
        }
    }
}

