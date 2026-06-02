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
            if (string.IsNullOrWhiteSpace(f) || string.IsNullOrWhiteSpace(l))
            {
                return " Първото име и фамилията на автора са задължителни";
            }
            f = f.Trim();
            l = l.Trim();   
            bool authorExists = await IsAuthorExisting(f, l);
            if (authorExists)
            {
                return $"Грешка: Авторът '{f} {l}' вече съществува ";
            }    
            Author a = new Author();
            a.FirstName = f;
            a.LastName = l;

            DbContext.Authors.Add(a);
            await DbContext.SaveChangesAsync();

            return $"Автор с Id {a.Id} е създаден успешно";
        }
        public async Task<Author> GetByName(string name,string ls)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(ls))
            {
                return null;
            }

            return await DbContext.Authors
                .FirstOrDefaultAsync(a => a.FirstName == name.Trim() && a.LastName == ls.Trim());
        }
        public async Task<bool> IsAuthorExisting(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                return false;
            }

            return await DbContext.Authors
                .AnyAsync(a => a.FirstName == firstName.Trim() && a.LastName == lastName.Trim());
        }
    }
}
