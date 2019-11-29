using P212Lab_11_29_19.Classes;
using System;
using System.Windows.Forms;

namespace P212Lab_11_29_19.Forms
{
    public partial class AllBooksForm : Form
    {
        public AllBooksForm()
        {
            InitializeComponent();
        }

        private void AllBooksForm_Load(object sender, EventArgs e)
        {
            Database database = new Database("myDb");
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = database.GetAllBooks();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            Database database = new Database("myDb");
            dataGridView1.DataSource = database.FindBook(tbx_Name.Text);
        }
    }
}
