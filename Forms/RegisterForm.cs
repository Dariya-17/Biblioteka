using Controller;
using Data.Entities;
using Data.Enums;
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
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();



        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            string password2 = textBox3.Text;

           
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Невалидно потребителско име");
                return;
            }
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(password2))
            {
                MessageBox.Show("Невалидна парола");
                return;
            }
            if (password2 != password)
            {
                MessageBox.Show("Паролите не съвпадат!");
                return;
            }

            Controller.RegisterLoginController controller = new Controller.RegisterLoginController();

           
            bool isTaken = await controller.IsUsernameTaken(username);
            if (isTaken)
            {
                MessageBox.Show("Потребителското име вече е заето");
                textBox1.Clear();           
                return; 
            }

            RoleType role = RoleType.Admin;
            role = RoleType.Admin;
            User user = new User
            {
               UserName = username,
                Password = password,
               Role = role
            };
            await controller.RegisterAdmin(username, password, role);
            DialogResult = DialogResult.OK;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
