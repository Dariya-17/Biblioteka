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
    public partial class RegisterReader : Form
    {
        private readonly AuthController controller = new AuthController();
        private readonly ReaderController readerController = new ReaderController();

        public RegisterReader()
        {
            InitializeComponent();
          
        }

        private void RegisterReader_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            string password2 = textBox3.Text;
            string fnama = textBox4.Text;
            string lname = textBox5.Text;
            string email = textBox6.Text;
            string phone = textBox7.Text;

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
            RoleType role = RoleType.Reader;
          
            if(await controller.RegisterAsync(username, password, fnama, lname, email, phone, role)==true)
            {
                DialogResult = DialogResult.OK;
                MessageBox.Show("Успешна регистрация!");    
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
