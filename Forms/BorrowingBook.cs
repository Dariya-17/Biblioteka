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
        .Where(b => b.ReaderId == Form1.LoggedInReader.Id)
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
            if (string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show(" въведете ID на книгата");
                return;
            }
            int bookId = int.Parse(textBox4.Text);
            int readerId = Form1.LoggedInReader.Id;
            BorrowingController borrowingController = new BorrowingController();
            bool isSuccess = await borrowingController.BorrowBook(readerId, bookId);
            if (isSuccess)
            {
                MessageBox.Show("Книгата беше заета успешно");

            }
            else
            {
                MessageBox.Show(" Книгата не съществува или няма останали свободни бройки!");
            }
        }
        private void BorrowingBook_Load(object sender, EventArgs e)
        {
            if (Form1.LoggedInReader != null)
            {

                textBox1.Text = Form1.LoggedInReader.FirstName;
                textBox2.Text = Form1.LoggedInReader.LastName;
                textBox3.Text = Form1.LoggedInReader.Id.ToString();
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
