using Data;
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
    public partial class ReaderFF : Form
    {
        private readonly LibraryDbContext _context = new LibraryDbContext();
        public ReaderFF()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AvailableBooks f = new AvailableBooks();
            f.ShowDialog();
        }

        private void ReaderFF_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            BorrowingBook f = new BorrowingBook();
            f.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            ReturnedBook f = new ReturnedBook();
            f.ShowDialog();
        }
    }
}
