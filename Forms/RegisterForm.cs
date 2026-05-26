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
            //comboBox1.Items.Add("Admin");
            
            
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            string password2 = textBox3.Text;

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Невалидно потребителско име!");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                return;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Невалидна парола!");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                return;
            }
            if (string.IsNullOrWhiteSpace(password2))
            {
                MessageBox.Show("Невалидна парола!");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                return;
            }
            if (password2 != password)
            {
                MessageBox.Show("Грешна парола!");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                return;
            }
            AuthController controller = new AuthController();
            RoleType role = new RoleType();
           
             role = RoleType.Admin;
            
            User user = new User
            {
                UserName = username,
                Password = password,
                Role = role
            };
            await controller.RegisterAdminAsync(username, password, role);
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
    }
}
