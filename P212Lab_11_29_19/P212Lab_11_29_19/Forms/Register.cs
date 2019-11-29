using P212Lab_11_29_19.Classes;
using System;
using System.Web.Helpers;
using System.Windows.Forms;

namespace P212Lab_11_29_19.Forms
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void btn_Register_Click(object sender, EventArgs e)
        {
            var tbx = this.Controls;
            bool isFlag = true;
            foreach (var item in tbx)
            {
                var s = item.GetType().Name;
                if (s=="TextBox"&&String.IsNullOrEmpty(((TextBox)item).Text))
                {
                    MessageBox.Show($"Fill {((TextBox)item).Name.TrimStart('t','b','x','_')}");
                    isFlag = false;
                }
            }
            if (isFlag)
            {
                User user = new User
                {
                    Email = tbx_Email.Text,
                    Name = tbx_Name.Text,
                    Password = Crypto.HashPassword(tbx_Password.Text),
                    Surname = tbx_Surname.Text,
                    RoleType=RoleType.User
                };
                Database database = new Database("myDb");
                if (database.CheckUser(user.Email))
                {
                    database.AddUser(user);
                    MessageBox.Show("User Added Successfully");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("This Email is already Registered!");
                }
                
            }



        }
    }
}
