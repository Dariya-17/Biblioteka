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
            try
            {
                var authorsList = await _authorController.GetAllAuthors();
                dataGridView1.DataSource = authorsList.Select(a => new
                {
                    a.Id,
                    a.FirstName,
                    a.LastName
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message);
            }

        } 

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string firstName = textBox1.Text;
                string lastName = textBox2.Text; 
                string resultMessage = await _authorController.AddAuthor(firstName, lastName);
                MessageBox.Show(resultMessage);
                textBox1.Clear();
                textBox2.Clear();        
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (ex.Message.Contains("съществува"))
                {
                    textBox1.Clear();
                    textBox2.Clear();
                }
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
                string searchFirstName = textBox1.Text;
                string searchLastName = textBox2.Text;           
                Author foundAuthor = await _authorController.GetByName(searchFirstName, searchLastName);             
                var authorList = new List<Author> { foundAuthor };
                dataGridView1.DataSource = authorList.Select(a => new
                {
                    a.Id,
                    a.FirstName,
                    a.LastName,
                }).ToList();
            }
            catch (Exception ex)
            {         
                dataGridView1.DataSource = null;
                MessageBox.Show(ex.Message);
            }
        }
    }
}
