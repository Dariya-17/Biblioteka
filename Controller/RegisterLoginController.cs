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

        public async Task<User> Login(string username, string password)
        {

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Потребителското име и паролата са задължителни за вход!");
            }
            var user = await context.Users.Include(u => u.Borrowings)
                .ThenInclude(b => b.Book)
                .Include(u => u.Books)
                .FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);

            if (user == null)
            {
                throw new Exception("Грешно потребителско име или парола");
            }

            return user;
        }
        public async Task<string> RegisterAdmin(string username, string password,string pass2, RoleType role)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Всички полета са задължителни за администратор");
            }
            if(password != pass2)
            {
                throw new Exception("Паролите не съвпадат");
            }       
            if (await context.Users.AnyAsync(u => u.UserName == username))
            {
                return "Потребителското име вече е заето";
            }

            var newAdmin = new User
            {
                UserName = username.Trim(),
                Password = password,
                Role = RoleType.Admin
            };
            await context.Users.AddAsync(newAdmin);
            await context.SaveChangesAsync();
            return "Успешна регистрация";
        }
        public async Task<bool> IsUsernameTaken(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) return false;
            return await context.Users.AnyAsync(u => u.UserName == username.Trim());
        }
        public async Task<bool> Register(string username, string password, string password2, string firstName, string lastName, string email, string phone, RoleType role)
        {

            if (string.IsNullOrWhiteSpace(username))
            {
                throw new Exception("Невалидно потребителско име");
            }
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(password2))
            {
                throw new Exception("Невалидна парола");
            }

            if (password != password2)
            {
                throw new Exception("Паролите не съвпадат");
            }
            if (role == RoleType.Reader)
            {
                foreach (char c in firstName)
                {
                    if (char.IsDigit(c)) throw new Exception("Първото име не може да съдържа цифри");
                }
                foreach (char c in lastName)
                {
                    if (char.IsDigit(c)) throw new Exception("Фамилното име не може да съдържа цифри");
                }
                if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                {
                    throw new Exception("Името и фамилията са задължителни за регистрация на читател");
                }
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phone))
                {
                    throw new Exception("Имейлът и телефонният номер са задължителни ");
                }
                if (!email.Contains("@"))
                {
                    throw new Exception("Невалиден имейл адрес! Трябва да съдържа символа '@'.");
                }
            }
            if (await context.Users.AnyAsync(u => u.UserName == username.Trim()))
            {
                throw new Exception("Потребителското име вече е заето.");
            }
            var newUser = new User
            {
                UserName = username.Trim(),
                Password = password,
                Role = role
            };
            await context.Users.AddAsync(newUser);
            await context.SaveChangesAsync();
            if (role == RoleType.Reader)
            {
                var newReader = new Reader
                {
                    FirstName = firstName.Trim(),
                    LastName = lastName.Trim(),
                    Email = email.Trim(),
                    PhoneNumber = phone.Trim(),
                    UserId = newUser.Id
                };

                await context.Readers.AddAsync(newReader);
                await context.SaveChangesAsync();
            }
            return true;
        }
    }
}



