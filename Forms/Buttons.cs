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
    public partial class Buttons : Form
    {
        public Buttons()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegisterForm form2 = new RegisterForm();
            DialogResult res = form2.ShowDialog();
            if (res == DialogResult.OK)
            {
                MessageBox.Show("Успешна регистрация!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegisterReader form2 = new RegisterReader();
            DialogResult res = form2.ShowDialog();
            if (res == DialogResult.OK)
            {
                MessageBox.Show("Успешна регистрация!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Buttons_Load(object sender, EventArgs e)
        {

        }
    }
}
