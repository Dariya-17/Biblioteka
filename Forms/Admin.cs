using Controller;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class Admin : Form
    {
        private readonly AuthorController authorController = new AuthorController();
        private readonly GenreController genreController = new GenreController();
        public Admin()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            List<Author> list = await authorController.GetAllAuthors();
            List<Genre> list1 = await genreController.GetAllAsync();
            ManageBooks f = new ManageBooks(list,list1);
            f.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ManageAuthors f = new ManageAuthors();
            f.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ManageGenre f = new ManageGenre();
            f.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ManageBorrowings f = new ManageBorrowings();
            f.ShowDialog();
        }

        private void Admin_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
