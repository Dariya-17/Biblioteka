using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class AuthorController
    {
        LibraryDbContext DbContext = new LibraryDbContext();
        public AuthorController()
        {

        }
        public AuthorController(LibraryDbContext c)
        {
            this.DbContext= c;
        }
        public async Task<List<Author>> GetAllAuthors()
        {
            return await DbContext.Authors.ToListAsync();
        }
        public async Task<Author> GetAuthorById(int id)
        {
            return await DbContext.Authors.FindAsync(id);
        }
        public async Task<string> AddAuthor(string f,string l)
        {
           Author a=new Author();
            a.FirstName = f;
            a.LastName = l;
            DbContext.Authors.Add(a);
            await DbContext.SaveChangesAsync();
            return $"Автор с Id {a.Id} е създаден успешно";
        }
        public async Task<Author> GetByName(string name,string ls)
        {
            return await DbContext.Authors
                .FirstOrDefaultAsync(a => a.FirstName == name && a.LastName == ls);
        }
        public async Task<bool> IsAuthorExisting(string firstName, string lastName)
        { 
            return await DbContext.Authors.AnyAsync(a => a.FirstName == firstName && a.LastName == lastName);
        }
    }
}
