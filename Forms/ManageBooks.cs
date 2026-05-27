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
        //private async void LoadDropdownData()
        //{
        //    try
        //    {

        //        var authors = await author.GetAllAuthors();
        //        comboBox1.DataSource = authors;
        //        comboBox1.DisplayMember = "FirstName " + "LastName";
        //        comboBox1.DisplayMember = "LastName "/* + "LastName"*/;
        //        //comboBox1.ValueMember = "Id";
        //        //comboBox1.SelectedIndex = -1;
        //        var genres = await genre.GetAllAsync();
        //        comboBox2.DataSource = genres;
        //        comboBox2.DisplayMember = "Name";
        //        //comboBox2.ValueMember = "Id";
        //        //comboBox2.SelectedIndex = -1;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Грешка при зареждане  {ex.Message}");
        //    }
        //}

        private void ManageBooks_Load(object sender, EventArgs e)
        {

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {

                var booksList = await book.GetAllAsync();
                dataGridView1.DataSource = booksList.Select(b => new
                {
                    b.Id,
                    b.Title,
                    b.AvailableCopies,
                    b.Author.FirstName,
                    b.Genre.Name
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Грешка при извеждане на книгите: {ex.Message}");
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Моля, въведете заглавие на книгата!");
                return;
            }
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Моля, изберете автор!");
                return;
            }
            if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Моля, изберете жанр!");
                return;
            }
            if (int.Parse(textBox1.Text) < 0)
            {
                MessageBox.Show("Моля, въведете валиден брой копия (цяло положително число)!");
                return;
            }
            string[] arr = comboBox1.SelectedItem.ToString().Split(' ');
            int authorId = (await author.GetByNameAsync(arr[0], arr[1])).Id;
            int genreId = genre.GetByNameID(comboBox2.SelectedItem.ToString()).Id;
            string title = textBox2.Text;
            await book.AddAsync(title, authorId, genreId, int.Parse(textBox1.Text));
            MessageBox.Show("Книгата беше добавена успешно!");

            textBox1.Clear();
            textBox2.Clear();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            string searchTitle = textBox2.Text;
            BooksController bookController = new BooksController();
            List<Books> foundBooks = await bookController.GetByTitleAsync(searchTitle);
            dataGridView1.DataSource = foundBooks.Select(b => new
            {
                b.Id,
                b.Title,
                b.Author.FirstName,
                b.Genre.Name,
                b.AvailableCopies
            }).ToList();
        }

        private async void button5_Click(object sender, EventArgs e)
        {
          
            if (string.IsNullOrEmpty(textBox3.Text)) 
            {
                MessageBox.Show(" въведете ID на книгата, която искате да изтриете");
                return;
            }        
            int bookId = int.Parse(textBox3.Text);   
            BooksController bookController = new BooksController();
            bool isDeleted = await bookController.DeleteByIdAsync(bookId);
            if (isDeleted)
            {
                MessageBox.Show("Книгата беше изтрита успешно от системата!");
               textBox3.Clear();             
            }
            else
            {
                MessageBox.Show(" Не е намерена книга с такова ID ");
            }
        }
    }
    }

