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
        private readonly Controller.RegisterLoginController controller = new Controller.RegisterLoginController();
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
                MessageBox.Show("Паролите не съвпадат");              
                return;
            }        
            Controller.RegisterLoginController controller = new Controller.RegisterLoginController();       
            bool isTaken = await controller.IsUsernameTaken(username);
            if (isTaken)
            {
                MessageBox.Show("Потребителското име вече е заето.");
                textBox1.Clear();              
                return; 
            }
            RoleType role = RoleType.Reader;         
            if (await controller.RegisterAsync(username, password, fnama, lname, email, phone, role) == true)
            {
                MessageBox.Show("Успешна регистрация");
                DialogResult = DialogResult.OK;          
                this.Close(); 
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
