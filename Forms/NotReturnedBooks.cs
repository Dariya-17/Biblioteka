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
    public partial class NotReturnedBooks : Form
    {
        BorrowingController context = new BorrowingController();
        public NotReturnedBooks()
        {
            InitializeComponent();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new LibraryDbContext())
                {
                    DateTime deadlineDate = DateTime.Now.AddDays(-30);

                    var overdueBorrowings = await context.Borrowings
                        .Include(b => b.Reader)
                        .Include(b => b.Book)
                        .Where(b => b.ReturnedDate == null && b.BorrowedDate < deadlineDate)
                        .Select(b => new
                        {
                            b.Id,
                            b.Reader.FirstName,
                            b.Reader.LastName,
                            b.Reader.PhoneNumber,
                            b.Book.Title,
                            b.BorrowedDate
                        })
                        .ToListAsync();

                    dataGridView1.DataSource = overdueBorrowings;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    if (overdueBorrowings.Count == 0)
                    {
                        MessageBox.Show("Няма клиенти със закъснения над 30 дни.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Грешка: {ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void NotReturnedBooks_Load(object sender, EventArgs e)
        {

        }
    }
}
