using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace P212Lab_11_29_19
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btn_Show_Click(object sender, EventArgs e)
        {
            //qosul
            //query
            //response
            //close;

            string conStr = ConfigurationManager.ConnectionStrings["QAYA"].ConnectionString;

            using (SqlConnection sqlConnection = new SqlConnection(conStr))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand("SELECT*FROM Students", sqlConnection))
                {
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        List<Student> students = new List<Student>();
                        while (dataReader.Read())
                        {
                            students.Add(new Student
                            {
                                Id = (int)dataReader["Id"],
                                Name = dataReader["Name"].ToString(),
                                Surname = dataReader["Surname"].ToString(),
                                Email = dataReader["Email"].ToString()
                            });
                        }
                        dataGridView1.DataSource = students;
                    }
                }

            }
        }
    }
}
