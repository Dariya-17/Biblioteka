using Controller;
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
    public partial class AvailableBooks : Form
    {
        private readonly BooksController _booksController = new BooksController();

        public AvailableBooks()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var allBooks = await _booksController.GetAll();

                var freeBooks = allBooks
                    .Where(b => b.AvailableCopies > 0)
                    .Select(b => new
                    {
                        b.Id,
                        b.Title,
                        b.Author.FirstName,
                        b.Author.LastName,
                        b.Genre.Name,
                        b.AvailableCopies
                    }).ToList();
                dataGridView1.DataSource = freeBooks;
                if (freeBooks.Count == 0)
                {
                    MessageBox.Show("В момента няма свободни книги в библиотеката.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AvailableBooks_Load(object sender, EventArgs e)
        {

        }
    }
}
