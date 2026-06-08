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
            try
            {
                string username = textBox1.Text;
                string password = textBox2.Text;
                string password2 = textBox3.Text;
                string fname = textBox4.Text;
                string lname = textBox5.Text;
                string email = textBox6.Text;
                string phone = textBox7.Text;
                RoleType role = RoleType.Reader;    
                bool isSuccess = await controller.Register(username, password, password2, fname, lname, email, phone, role);
                if (isSuccess)
                {
                    MessageBox.Show("Успешна регистрация");
                    DialogResult = DialogResult.OK;
                 this.Close();
                }
            }
            catch (Exception ex)
            {
         
                MessageBox.Show(ex.Message);   
                if (ex.Message.Contains("заето"))
                {
                    textBox1.Clear();
                    return;
                }
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
