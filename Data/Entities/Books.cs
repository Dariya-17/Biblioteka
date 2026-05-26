using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
   public class Books
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        [ForeignKey(nameof(AuthorId))]
        public int AuthorId { get; set; }
        [InverseProperty(nameof(Author.Books))]
        public Author Author { get; set; }
        [ForeignKey(nameof(GenreId))]
        public int GenreId { get; set; }
        [InverseProperty(nameof(Genre.Books))]
        public Genre Genre { get; set; }
        public int AvailableCopies { get; set; }    
        public ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
    }
}
