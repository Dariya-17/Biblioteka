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
    public partial class AllClients : Form
    {
        ReaderController controller = new ReaderController();
        public AllClients()
        {
            InitializeComponent();
        }

        private void AllClients_Load(object sender, EventArgs e)
        {

        }
        private async Task RefreshAdminBorrowingsGrid()
        {
            try
            {
                using (var context = new LibraryDbContext())
                {
                    var allActiveBorrowings = await context.Borrowings
                        .Include(b => b.Reader)
                        .Include(b => b.Book)
                        .Where(b => b.ReturnedDate == null|| b.ReturnedDate != null)
                        .Select(b => new
                        {
                            b.Id,
                            Име_Читател = b.Reader.FirstName ,
                            b.Reader.LastName,
                            b.Reader.PhoneNumber,
                            b.Book.Title,
                            b.BorrowedDate
                        })
                        .ToListAsync();
                    dataGridView1.DataSource = allActiveBorrowings;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            RefreshAdminBorrowingsGrid();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
