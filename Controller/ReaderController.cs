using Data.Entities;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Versioning;
using Data.Enums;

namespace Controller
{
    public class ReaderController
    {
        private readonly LibraryDbContext context = new LibraryDbContext();
        public ReaderController()
        {

        }
        public ReaderController(LibraryDbContext c)
        {
            this.context = c;
        }
        public async Task<List<Reader>> GetAll() 
        {
            return await context.Readers.ToListAsync();

        }
        //public async Task<string> Add(string firstName, string lastName, string email, string phone, string username, string password)
        //{
           
        //    if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
        //    {
        //        return "Имената на читателя са задължителни";
        //    } 
        //    firstName = firstName.Trim();
        //    lastName = lastName.Trim();

        //    if (firstName.Contains("1,2,3,4,5,6,7,8,9,0") || lastName.Contains("1,2,3,4,5,6,7,8,9,0"))
        //    {
        //        return "Името и фамилията не могат да съдържат цифри";
        //    }
           
        //    if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phone))
        //    {
        //        return "Имейлът и телефонният номер са задължителни полета";
        //    }

        //    if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        //    {
        //        return "Потребителското име и паролата са задължителни";
        //    }

        //    email = email.Trim();
        //    phone = phone.Trim();
        //    username = username.Trim();

        //    bool isUsernameTaken = await context.Users.AnyAsync(u => u.UserName == username);
        //    if (isUsernameTaken)
        //    {
        //        return $"Потребителското име {username} вече е заето";
        //    }

         
        //    bool readerExists = await context.Readers.AnyAsync(r => r.Email == email || (r.FirstName == firstName && r.LastName == lastName));
        //    if (readerExists)
        //    {
        //        return "Вече съществува читател с тези имена или този имейл адрес";
        //    }

          
        //    var newUser = new User
        //    {
        //        UserName = username,
        //        Password = password,
        //        Role = RoleType.Reader
        //    };
        //    await context.Users.AddAsync(newUser);
        //    await context.SaveChangesAsync(); 

         
        //    var newReader = new Reader
        //    {
        //        FirstName = firstName,
        //        LastName = lastName,
        //        Email = email,
        //        PhoneNumber = phone,
        //        UserId = newUser.Id 
        //    };
        //    await context.Readers.AddAsync(newReader);
        //    await context.SaveChangesAsync(); 

        //    return "Читателят беше регистриран успешно";
        //}
        public async Task<List<Reader>> GetByName(string Fname,string lname)
        {
            if (string.IsNullOrWhiteSpace(Fname)|| string.IsNullOrWhiteSpace(lname)) return await GetAll();
            return await context.Readers
                .Where(r => r.FirstName==Fname && r.LastName==lname)
                .ToListAsync();
        }
  
    }
}

