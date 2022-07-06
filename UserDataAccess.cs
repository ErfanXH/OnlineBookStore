using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace DataAccess
{
    public class UserDataAccess
    {
        public BookDataAccess bookDataAccess = new BookDataAccess();
        public ObservableCollection<User> Users { get; set; }
        public UserDataAccess()
        {
            Users = new ObservableCollection<User>();
            ReadUsers();
        }
        private void ReadUsers()//for Data base
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lenovo\source\repos\HomePage\DataAccess\user.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();
            string command;
            command = "Select * From T_User";
            SqlDataAdapter adapter = new SqlDataAdapter(command, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);
            for(int i = 0; i < data.Rows.Count; i++)
            {
                string[] array = data.Rows[i][7].ToString().Split('-');
                List<int> IDs = new List<int>();
                if (array.Length > 0 && array[0] != "")
                {
                    foreach (string item in array)
                    {
                        IDs.Add(int.Parse(item));
                    }
                }

                User U = new User
                {
                    FirstName = data.Rows[i][0].ToString(),
                    LastName = data.Rows[i][1].ToString(),
                    PhoneNumber = data.Rows[i][2].ToString(),
                    Email = data.Rows[i][3].ToString(),
                    Password = data.Rows[i][4].ToString(),
                    AccountBalance = float.Parse(data.Rows[i][5].ToString()),
                    VIP = bool.Parse(data.Rows[i][6].ToString()),
                };
                if (array.Length > 0 && array[0] != "")
                {
                    foreach (var book in bookDataAccess.Books)
                    {
                        if (IDs.Contains(book.ID))
                            U.Books.Add(book);
                    }
                }
                Users.Add(U);
            }
            connection.Close();
        }
        public void AddUser(User U)
        {
            Users.Add(U);
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lenovo\source\repos\HomePage\DataAccess\user.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();

            string IDs = "";
            for (int i = 0; i < U.Books.Count; i++)
            {
                IDs += U.Books[i].ID;
                if (i != U.Books.Count - 1)
                    IDs += "-";
            }

            string command = "Insert into T_User values('"+ U.FirstName +"','"+ U.LastName +"','"+ U.PhoneNumber +"','"+ U.Email +"','"+ U.Password +"','"+ U.AccountBalance +"','"+ U.VIP +"'. '"+IDs+"' )";
            SqlCommand com = new SqlCommand(command, connection);
            com.ExecuteNonQuery();
            connection.Close();
        }
        public void EditUser(User NewUser,User OldUser)
        {
            int Index = Users.IndexOf(OldUser);
            Users[Index] = NewUser;
        }
    }
}
