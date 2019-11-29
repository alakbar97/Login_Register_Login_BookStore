using P212Lab_11_29_19.Classes;
using System;
using System.Windows.Forms;

namespace P212Lab_11_29_19.Forms
{
    public partial class BookForm : Form
    {
        public BookForm()
        {
            InitializeComponent();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            var tbx = this.Controls;
            bool isFlag = true;
            foreach (var item in tbx)
            {
                var s = item.GetType().Name;
                if (s == "TextBox" && String.IsNullOrEmpty(((TextBox)item).Text))
                {
                    MessageBox.Show($"Fill {((TextBox)item).Name.TrimStart('t', 'b', 'x', '_')}");
                    isFlag = false;
                }
            }
            if (isFlag && short.TryParse(tbx_Page.Text, out short result))
            {
                Database database = new Database("myDb");
                database.AddBook(new Book
                {
                    Author = tbx_Author.Text,
                    Name = tbx_Name.Text,
                    PageSize = short.Parse(tbx_Page.Text)
                });
                MessageBox.Show("Success ! ");
            }
            else
            {
                MessageBox.Show("Enter Valid Page Size");
            }
        }

        private void BookForm_Load(object sender, EventArgs e)
        {

        }

        private void btn_Show_Click(object sender, EventArgs e)
        {
            AllBooksForm allBooksForm = new AllBooksForm();
            allBooksForm.ShowDialog();
        }
    }
}
