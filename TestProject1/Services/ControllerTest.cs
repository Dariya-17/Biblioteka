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
        public async Task Register()
        {
            var context = TestDb.CreateContext();
            string username = "newReader";
            string password = "password123";
            string firstName = "Иван";
            string lastName = "Иванов";
            string email = "ivan@abv.bg";
            string phone = "0888888888";
            RoleType role = RoleType.Reader;
            RegisterLoginController controller = new RegisterLoginController(context);
            bool result = await controller.Register(username, password, firstName, lastName, email, phone, role);
            Assert.IsTrue(result);                           
            var userInDb = await context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            Assert.IsNotNull(userInDb);
            Assert.AreEqual(role, userInDb.Role);
            var readerInDb = await context.Readers.FirstOrDefaultAsync(r => r.UserId == userInDb.Id);
            Assert.IsNotNull(readerInDb);
            Assert.AreEqual(firstName, readerInDb.FirstName);
            Assert.AreEqual(lastName, readerInDb.LastName);
        }
        [Test]
        public async Task Register_False()
        {
            var context = TestDb.CreateContext();
            RegisterLoginController controller = new RegisterLoginController(context);
            var existingUser = new User { UserName = "existingUser", Password = "123", Role = RoleType.Admin };
            await context.Users.AddAsync(existingUser);
            await context.SaveChangesAsync();
            string username = "existingUser";
            string password = "newpassword";
            string firstName = "Петър";
            string lastName = "Петров";
            string email = "pesho@abv.bg";
            string phone = "0899999999";
            RoleType role = RoleType.Reader;
            bool result = await controller.Register(username, password, firstName, lastName, email, phone, role);
            Assert.IsFalse(result); 
        }
    }
}

