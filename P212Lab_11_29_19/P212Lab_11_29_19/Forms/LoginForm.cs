using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using P212Lab_11_29_19.Classes;

namespace P212Lab_11_29_19.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            Database database = new Database("myDb");
            if (database.CheckUser(tbx_Email.Text,tbx_Password.Text))
            {
                BookForm book = new BookForm();
                book.ShowDialog();
            }
            else
            {
                MessageBox.Show("Email or Password is not Correct ! ");
            }
        }
    }
}
