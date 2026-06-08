using Controller;
using Data.Entities;
using Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class RegisterForm : Form
    {
        private readonly RegisterLoginController controller = new RegisterLoginController();
        public RegisterForm()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string username = textBox1.Text;
                string password = textBox2.Text;
                string password2 = textBox3.Text;
                RoleType role = RoleType.Admin;         
               string resultMessage = await controller.RegisterAdmin(username, password,password2, role);

                if (resultMessage == "Успешна регистрация")
                {
                    MessageBox.Show(resultMessage);
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(resultMessage);
                    textBox1.Clear();
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

        private void RegisterForm_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
