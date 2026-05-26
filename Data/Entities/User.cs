using Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
   public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public RoleType Role { get; set; }
        public ICollection<Books> Books { get; set; } = new List<Books>();

        public ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
    }
}
