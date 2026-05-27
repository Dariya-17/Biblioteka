using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.Helpers
{
 public class TestDb
    {
        public static LibraryDbContext CreateContext()
        {
            var option = new DbContextOptionsBuilder<LibraryDbContext>().
                UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            LibraryDbContext context = new LibraryDbContext(option);
            context.Database.EnsureCreated();   
            return context;
        }
    }
}
