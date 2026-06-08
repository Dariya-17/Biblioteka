using Controller;
using Data;
using Data.Entities;
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
    public partial class BorrowingBook : Form
    {
        private readonly BorrowingController _borrowingController = new BorrowingController();
        public BorrowingBook()
        {
            InitializeComponent();

        }
        private async void button1_Click(object sender, EventArgs e)
        {
            using (var context = new LibraryDbContext())
            {

          var userBorrowings = await context.Borrowings
        .Include(b => b.Reader)
        .Where(b => b.ReaderId == Library.LoggedInReader.Id)
        .Select(b => new
        {
            b.Id,
           b.Book.Title,
             b.Reader.FirstName,
           b.BorrowedDate,
            b.ReturnedDate
        })
        .ToListAsync();
                dataGridView1.DataSource = userBorrowings;
            }
        }
        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int readerId = Library.LoggedInReader.Id;            
                string bookInput = textBox4.Text;       
                DateTime selectedDate = dateTimePicker1.Value;          
                bool isSuccess = await _borrowingController.BorrowBook(readerId, bookInput, selectedDate);
                if (isSuccess)
                {
                    MessageBox.Show("Книгата беше заета успешно");
                    textBox4.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BorrowingBook_Load(object sender, EventArgs e)
        {
            if (Library.LoggedInReader != null)
            {

                textBox1.Text = Library.LoggedInReader.FirstName;
                textBox2.Text = Library.LoggedInReader.LastName;
                textBox3.Text = Library.LoggedInReader.Id.ToString();
                textBox1.ReadOnly = true;
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
            }
            else
            {
                MessageBox.Show(" Няма зареден читател ");
                this.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
