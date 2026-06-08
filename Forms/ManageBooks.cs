using Controller;
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
    public partial class ManageBooks : Form
    {
        public readonly AuthorController author = new AuthorController();
        public readonly GenreController genre = new GenreController();
        public readonly BooksController book = new BooksController();
        public ManageBooks(List<Author> list, List<Genre> genre)
        {
            InitializeComponent();

            foreach (var a in list)
            {
                comboBox1.Items.Add($"{a.FirstName} {a.LastName}");
            }
            foreach (var g in genre)
            {
                comboBox2.Items.Add(g.Name);
            }


        }

        private void ManageBooks_Load(object sender, EventArgs e)
        {

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var booksList = await book.GetAll();
                dataGridView1.DataSource = booksList.Select(b => new
                {
                    b.Id,
                    b.Title,
                   b.AvailableCopies,
                    b.Author.FirstName,
               b.Author.LastName,
                  b.Genre.Name
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
   
        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
             
                if (comboBox1.SelectedItem == null)
                {
                    throw new Exception("Моля, изберете автор от списъка");
                }
                if (comboBox2.SelectedItem == null)
                {
                    throw new Exception("Моля, изберете жанр от списъка");
                }
                string title = textBox2.Text;    
                string[] arr = comboBox1.SelectedItem.ToString().Split(' ');
                var foundAuthor = await author.GetByName(arr[0], arr[1]);
                var foundGenre = await genre.GetByNameID(comboBox2.SelectedItem.ToString());            
                int copies = int.TryParse(textBox1.Text, out int parsedCopies) ? parsedCopies : -1;           
                string resultMessage = await book.AddAsync(title, foundAuthor.Id, foundGenre.Id, copies);

                MessageBox.Show(resultMessage);
                textBox1.Clear();
                textBox2.Clear();                    
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string searchTitle = textBox2.Text;          
                List<Books> foundBooks = await book.GetByTitle(searchTitle);
                if (foundBooks == null || foundBooks.Count == 0)
                {
                    throw new Exception("Няма намерени книги с това заглавие");
                }
                dataGridView1.DataSource = foundBooks.Select(b => new
                {
                    b.Id,
                    b.Title,
                  b.Author.FirstName,
                 b.Author.LastName,
                   b.Genre.Name,
                b.AvailableCopies
                }).ToList();
            }
            catch (Exception ex)
            {
                dataGridView1.DataSource = null;
                MessageBox.Show(ex.Message);
            }
        }

        private async void button5_Click(object sender, EventArgs e)
        {

            try
            {
            
                if (!int.TryParse(textBox3.Text, out int bookId) || bookId <= 0)
                {
                    throw new Exception("Моля, въведете валидно цифрово ID на книга за изтриване!");
                }
  
                bool isDeleted = await book.DeleteById(bookId);
                if (isDeleted)
                {
                    MessageBox.Show("Книгата беше изтрита успешно!");
                    textBox3.Clear();
                }
                else
                {
                    throw new Exception("Не е намерена книга с такова ID ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}

