using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Helpers;

namespace P212Lab_11_29_19.Classes
{
    public class Database
    {
        private string connectionString;
        private List<Book> books;
        private List<Book> results;


        public Database(string databaseName)
        {
            connectionString = ConfigurationManager.ConnectionStrings[databaseName].ConnectionString;
            results = new List<Book>();
            books = new List<Book>();
        }

        internal bool CheckUser(string email)
        {
            bool isFlag = true;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand($"SELECT Id FROM Users WHERE Email='{email.ToLower()}'", sqlConnection))
                {
                    using (SqlDataReader sqlData = sqlCommand.ExecuteReader())
                    {
                        while (sqlData.Read())
                        {
                            if (sqlData["Id"] != null)
                            {
                                isFlag = false;
                            }
                        }
                    }
                }
            }
            return isFlag;
        }

        internal List<Book> FindBook(string name)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand($"SELECT Name,Author,PageSize FROM Books WHERE Name LIKE '%{name}%'", sqlConnection))
                {
                    using (SqlDataReader sqlData = sqlCommand.ExecuteReader())
                    {
                        while (sqlData.Read())
                        {
                            results.Add(new Book
                            {
                                Author = sqlData["Author"].ToString(),
                                Name = sqlData["Name"].ToString(),
                                PageSize = (short)sqlData["PageSize"]
                            });
                        }
                    }
                }
            }
            return results;
        }

        internal List<Book> GetAllBooks()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand($"SELECT Name,Author,PageSize FROM Books", sqlConnection))
                {
                    using (SqlDataReader sqlData = sqlCommand.ExecuteReader())
                    {
                        while (sqlData.Read())
                        {
                            books.Add(new Book
                            {
                                Author = sqlData["Author"].ToString(),
                                Name = sqlData["Name"].ToString(),
                                PageSize = (short)sqlData["PageSize"]
                            });
                        }
                    }
                }
            }
            return books;
        }

        internal void AddBook(Book book)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand($"INSERT INTO Books (Name,Author,PageSize)" +
                    $" VALUES ('{book.Name}','{book.Author}','{book.PageSize}')", sqlConnection))
                {
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        internal bool CheckUser(string email, string pass)
        {
            bool isFlag = false;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand($"SELECT Email,Password FROM Users WHERE Email='{email.ToLower()}'", sqlConnection))
                {
                    using (SqlDataReader sqlData = sqlCommand.ExecuteReader())
                    {
                        while (sqlData.Read())
                        {
                            if (sqlData["Email"].ToString().ToLower() == email.ToLower() && Crypto.VerifyHashedPassword(sqlData["Password"].ToString(), pass))
                            {
                                isFlag = true;
                            }
                        }
                    }
                }
            }
            return isFlag;
        }

        internal void AddUser(User user)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand($"INSERT INTO Users (Name,Surname,Email,Password,RoleType)" +
                    $" VALUES ('{user.Name}','{user.Surname}','{user.Email.ToLower()}','{user.Password}','{(byte)user.RoleType}')", sqlConnection))
                {
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
}
