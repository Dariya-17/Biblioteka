using Controller;
using Data;
using Data.Entities;
using Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace Forms
{
    public partial class Form1 : Form
    {
        public static Reader LoggedInReader { get; set; }

        private readonly LibraryDbContext _context = new LibraryDbContext();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            Controller.RegisterLoginController authController = new Controller.RegisterLoginController();
            User loggedInUser = await authController.LoginAsync(username, password);

            if (loggedInUser != null)
            {
                if (loggedInUser.Role == RoleType.Reader)
                {
                    Form1.LoggedInReader = await _context.Readers
                         .FirstOrDefaultAsync(r => r.UserId == loggedInUser.Id);
                    ReaderFF readerForm = new ReaderFF();
                    readerForm.ShowDialog();
                }
                else if (loggedInUser.Role == RoleType.Admin)
                {
                    Admin adminForm = new Admin();
                    adminForm.ShowDialog(); 
                }    
            }
            else
            {
                MessageBox.Show("Грешно потребителско име или парола!");
            }
            textBox1.Clear();
           textBox2.Clear();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Buttons button = new Buttons();
            button.ShowDialog();
            this.Show();
        }
    }
}
