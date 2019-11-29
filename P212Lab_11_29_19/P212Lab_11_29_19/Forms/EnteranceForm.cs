using P212Lab_11_29_19.Classes;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace P212Lab_11_29_19.Forms
{
    public partial class EnteranceForm : Form
    {
        public EnteranceForm()
        {
            InitializeComponent();
        }

        private void btn_Register_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
        }

        private void EnteranceForm_Load(object sender, EventArgs e)
        {
            Database database = new Database("myDb");
            if (database.CheckUser("admin@gmail.com"))
            {
                database.AddUser(new User
                {
                    Email = ConfigurationManager.AppSettings["email"],
                    Name = ConfigurationManager.AppSettings["name"],
                    Password = ConfigurationManager.AppSettings["pass"],
                    RoleType = RoleType.Admin,
                    Surname = ConfigurationManager.AppSettings["surname"]
                });
            }
        }
    }
}
