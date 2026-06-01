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
    public partial class ManageGenre : Form
    {
        public readonly GenreController genre = new GenreController();
        public ManageGenre()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string j = textBox1.Text;
            if (string.IsNullOrEmpty(j))
            {
                MessageBox.Show("Невалидно име на жанр");
                return;
            }
            await genre.AddAsync(j);
            MessageBox.Show("Успешно добавяне на жанр");
            textBox1.Clear();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var list = await genre.GetAllAsync();
                dataGridView1.DataSource = list.Select(g => new { g.Id, g.Name }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при зареждане на жанровете " );
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ManageGenre_Load(object sender, EventArgs e)
        {

        }
    }
}
