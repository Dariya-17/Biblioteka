using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
  public class Borrowing
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(ReaderId))]
        public int ReaderId { get; set; }
        [InverseProperty(nameof(Reader.Borrowings))]
        public Reader Reader { get; set; }
        [ForeignKey(nameof(BookId))]
        public int BookId { get; set; }
        [InverseProperty(nameof(Books.Borrowings))]
        public Books Book { get; set; }
        public DateTime BorrowedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
    }
}
