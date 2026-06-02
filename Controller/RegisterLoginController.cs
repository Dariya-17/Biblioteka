using Data.Entities;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Data.Enums;
using System.ComponentModel.Design;

namespace Controller
{
    public class RegisterLoginController
    {
        private readonly LibraryDbContext context = new LibraryDbContext();


        public RegisterLoginController()
        {

        }
        public RegisterLoginController(LibraryDbContext c)
        {
            this.context = c;
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            return await context.Users.Include(u => u.Borrowings) 
                .ThenInclude(b => b.Book)   
                .Include(u => u.Books)        
                .FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
        }
        public async Task<string> RegisterAdminAsync(string username, string password, RoleType role)
        {     
                if (await context.Users.AnyAsync(u => u.UserName == username))
                {
                    return "Потребителското име вече е заето";
                }
                var newAdmin = new User
                {
                    UserName = username,
                    Password = password,
                    Role = RoleType.Admin 
                };
                await context.Users.AddAsync(newAdmin);
                await context.SaveChangesAsync();
                return "Успешна регистрация";       
        }
        public async Task<bool> IsUsernameTaken(string username)
        {
            return await context.Users.AnyAsync(u => u.UserName == username);
        }
        public async Task<bool> RegisterAsync(string username, string password, string firstName, string lastName, string email, string phone, RoleType role)
        { 
            if (await context.Users.AnyAsync(u => u.UserName == username))
            {
                return false;
            }    
            var newUser = new User
            {
                UserName = username,
                Password = password,
                Role = role
            };
            await context.Users.AddAsync(newUser);
            await context.SaveChangesAsync(); 
            if (role == RoleType.Reader)
            {
                var newReader = new Reader
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    PhoneNumber = phone,         
                    UserId = newUser.Id
                };

                await context.Readers.AddAsync(newReader);
                await context.SaveChangesAsync(); 
            }
            return true;
        }
       
    }
}


