using Data.Entities;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Versioning;

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
        public async Task Add(string firstName, string lastName, string email, string phone,string username,string password)
        {
            context.Readers.Add(new Reader { FirstName = firstName, LastName = lastName, Email = email, PhoneNumber = phone });
           context.Users.Add(new User { UserName=username,Password=password});
            context.SaveChanges();
        }
        public async Task<List<Reader>> GetByName(string Fname,string lname)
        {
            if (string.IsNullOrWhiteSpace(Fname)|| string.IsNullOrWhiteSpace(lname)) return await GetAll();
            return await context.Readers
                .Where(r => r.FirstName==Fname && r.LastName==lname)
                .ToListAsync();
        }
  
    }
}

