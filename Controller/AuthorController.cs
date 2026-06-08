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
            if (id <= 0)
            {
                throw new Exception("Моля, въведете валидно ID на автор!");
            }
            return await DbContext.Authors.FindAsync(id);
        }
        public async Task<string> AddAuthor(string f, string l)
        {
      
            if (string.IsNullOrWhiteSpace(f) || string.IsNullOrWhiteSpace(l))
            {
                throw new Exception("Първото име и фамилията на автора са задължителни");
            }
            f = f.Trim();
            l = l.Trim();
            bool authorExists = await IsAuthorExisting(f, l);
            if (authorExists)
            {
                throw new Exception($"Авторът '{f} {l}' вече съществува ");
            }

            Author a = new Author
            {
                FirstName = f,
                LastName = l
            };

            DbContext.Authors.Add(a);
            await DbContext.SaveChangesAsync();

            return $"Автор с Id {a.Id} е създаден успешно";
        }
        public async Task<Author> GetByName(string name,string ls)
        {
          
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(ls))
            {
                throw new Exception("Моля, въведете както име, така и фамилия ");
            }
            var author = await DbContext.Authors
                .FirstOrDefaultAsync(a => a.FirstName.Trim() == name.Trim() && a.LastName.Trim() == ls.Trim());

            if (author == null)
            {
                throw new Exception($"Няма намерен автор с имената '{name.Trim()} {ls.Trim()}'!");
            }

            return author;
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
