using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class LibraryDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Borrowing> Borrowings { get; set; }
        public DbSet<Reader> Readers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("nz.json");
            var config = builder.Build();
            string conString = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(conString);
        }
    }
}
