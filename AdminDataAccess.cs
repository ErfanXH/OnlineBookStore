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
    public class AdminDataAccess
    {
        public ObservableCollection<Admin> Admins { get; set; }
        public AdminDataAccess()
        {
            Admins = new ObservableCollection<Admin>();
            ReadAdmins();
        }
        private void ReadAdmins()//for Data base
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Amir\Documents\Project_AP\OnlineBookStore\DataBase\DatabaseOnlineBookStore.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();
            string command;
            command = "Select * From T_Admin";
            SqlDataAdapter adapter = new SqlDataAdapter(command, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);
            for(int i = 0; i < data.Rows.Count; i++)
            {
                Admin A = new Admin() { Username = data.Rows[i][0].ToString() };
                Admins.Add(A);
            }
            connection.Close();
        }
        public void AddAdmin(Admin A)
        {
            Admins.Add(A);
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Amir\Documents\Project_AP\OnlineBookStore\DataBase\DatabaseOnlineBookStore.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();
            string command;
            command = "insert into T_Admin values('"+ A.Username +"' )";
            SqlCommand com = new SqlCommand(command, connection);
            com.ExecuteNonQuery();
            connection.Close();
        }
    }
}
