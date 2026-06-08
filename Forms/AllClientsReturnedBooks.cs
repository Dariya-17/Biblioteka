using Controller;
using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class AllClientsReturnedBooks : Form
    {
        BorrowingController context = new BorrowingController();
        public AllClientsReturnedBooks()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new LibraryDbContext())
                {
                    var allReturnedBooks = await context.Borrowings
                        .Include(b => b.Reader)
                        .Include(b => b.Book)
                        .Where(b => b.ReturnedDate != null)
                        .OrderByDescending(b => b.ReturnedDate)
                        .Select(b => new
                        {
                            b.Id,
                            b.Reader.FirstName,
                            b.Reader.LastName,
                            b.Book.Title,
                            b.BorrowedDate,
                            b.ReturnedDate
                        })
                        .ToListAsync();
                    dataGridView1.DataSource = allReturnedBooks;
                    if (allReturnedBooks.Count == 0)
                    {
                        MessageBox.Show("Няма намерени върнати книги в цялата система.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Грешка при извеждане на върнатите книги: {ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
