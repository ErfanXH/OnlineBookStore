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
using Microsoft.Data.SqlClient;

namespace DataAccess
{
    public class UserDataAccess
    {
        public ObservableCollection<User> Users { get; set; }
        public UserDataAccess()
        {
            Users = new ObservableCollection<User>();
            ReadUsers();
        }
        private void ReadUsers()//for Data base
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Amir\Documents\Project_AP\OnlineBookStore\DataBase\DatabaseOnlineBookStore.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();
            string command;
            command = "Select * From T_User";
            SqlDataAdapter adapter = new SqlDataAdapter(command, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);
            for(int i = 0; i < data.Rows.Count; i++)
            {
                User U = new User
                {
                    FirtName = data.Rows[i][0].ToString(),
                    LastName = data.Rows[i][1].ToString(),
                    PhoneNumber = data.Rows[i][2].ToString(),
                    Email = data.Rows[i][3].ToString(),
                    Password = data.Rows[i][4].ToString(),
                    AccountBalance = float.Parse(data.Rows[i][5].ToString()),
                    VIP = bool.Parse(data.Rows[i][6].ToString()),
                };
                Users.Add(U);
            }
            connection.Close();
        }
        public void AddUser(User U)
        {
            Users.Add(U);
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Amir\Documents\Project_AP\OnlineBookStore\DataBase\DatabaseOnlineBookStore.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();
            string command = "Insert into T_User values('"+ U.FirtName +"','"+ U.LastName +"','"+ U.PhoneNumber +"','"+ U.Email +"','"+ U.Password +"','"+ U.AccountBalance +"','"+ U.VIP +"' )";
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
