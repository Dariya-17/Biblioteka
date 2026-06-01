using Controller;
using Data.Entities;
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
    public partial class ManageAuthors : Form
    {
        private readonly AuthorController _authorController = new AuthorController();
        public ManageAuthors()
        {
            InitializeComponent();
        }

        private void ManageAuthors_Load(object sender, EventArgs e)
        {

        }

        private async void button2_Click(object sender, EventArgs e)
        {

            await LoadAuthorsIntoGrid();


        }

        private async Task LoadAuthorsIntoGrid()
        {
            try
            {
                var authorsList = await _authorController.GetAllAuthors();
                dataGridView1.DataSource = authorsList.Select(a => new
                {
                    Номер = a.Id,
                    Име = a.FirstName,
                    Фамилия = a.LastName
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Грешка при извеждане на авторите: {ex.Message}");
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Моля, попълнете както името, така и фамилията на автора");
                return;
            }
            string resultMessage = await _authorController.AddAuthor(textBox1.Text, textBox2.Text);

            MessageBox.Show(resultMessage);

            textBox1.Clear();
            textBox2.Clear();

            await LoadAuthorsIntoGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            string searchFirstName = textBox1.Text; 
            string searchLastName = textBox2.Text;            
            AuthorController authorController = new AuthorController();          
            Author foundAuthor = await authorController.GetByNameAsync(searchFirstName, searchLastName); 
            if (foundAuthor != null)
            {
     
              var authorList = new List<Author> { foundAuthor };       
              dataGridView1.DataSource = authorList.Select(a => new
                {
                a.Id,
                 a.FirstName,
                 a.LastName,   
                }).ToList(); 
            }
            else
            { 
                dataGridView1.DataSource = null;
                MessageBox.Show("Няма намерен автор с тези имена!");
            }
        }
    }
}
