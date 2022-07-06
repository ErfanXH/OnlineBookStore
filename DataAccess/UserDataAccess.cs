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
        public ObservableCollection<Book> Books { get; set; }
        public UserDataAccess(ObservableCollection<Book> Books)
        {
            this.Books = Books;
            Users = new ObservableCollection<User>();
            ReadUsers();
        }
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
                    FirstName = data.Rows[i][0].ToString(),
                    LastName = data.Rows[i][1].ToString(),
                    PhoneNumber = data.Rows[i][2].ToString(),
                    Email = data.Rows[i][3].ToString(),
                    Password = data.Rows[i][4].ToString(),
                    AccountBalance = double.Parse(data.Rows[i][5].ToString()),
                    VIP = bool.Parse(data.Rows[i][6].ToString()),
                };
                Users.Add(U);
            }
            connection.Close();
            for(int i = 0; i < Users.Count; i++)
            {
                SqlConnection connection2 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Amir\Documents\Project_AP\OnlineBookStore\DataBase\DatabaseOnlineBookStore.mdf;Integrated Security=True;Connect Timeout=30");
                connection2.Open();
                command = "Select ID From T_UsersBooks Where Email = '" + Users[i].Email + "'; ";
                adapter = new SqlDataAdapter(command, connection2);
                data = new DataTable();
                adapter.Fill(data);
                for(int j = 0; j < data.Rows.Count; j++)
                {
                    int id_book = int.Parse(data.Rows[j][0].ToString());
                    Book book = Books.First(x => x.ID == id_book);
                    Users[i].Books.Add(book);
                }
                connection2.Close();
                SqlConnection connection3 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Amir\Documents\Project_AP\OnlineBookStore\DataBase\DatabaseOnlineBookStore.mdf;Integrated Security=True;Connect Timeout=30");
                connection3.Open();
                command = "Select ID From T_BasketOfGoods Where Email = '" + Users[i].Email + "'; ";
                adapter = new SqlDataAdapter(command, connection3);
                data = new DataTable();
                adapter.Fill(data);
                for(int j = 0; j < data.Rows.Count; j++)
                {
                    int id_book = int.Parse(data.Rows[j][0].ToString());
                    Book book = Books.First(x => x.ID == id_book);
                    Users[i].BasketOfGoods.Add(book);
                }
                connection3.Close();
                SqlConnection connection4 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Amir\Documents\Project_AP\OnlineBookStore\DataBase\DatabaseOnlineBookStore.mdf;Integrated Security=True;Connect Timeout=30");
                connection4.Open();
                command = "Select ID From T_BookMark Where Email = '" + Users[i].Email + "'; ";
                adapter = new SqlDataAdapter(command, connection4);
                data = new DataTable();
                adapter.Fill(data);
                for(int j = 0; j < data.Rows.Count; j++)
                {
                    int id_book = int.Parse(data.Rows[j][0].ToString());
                    Book book = Books.First(x => x.ID == id_book);
                    Users[i].BookMark.Add(book);
                }
                connection4.Close();
            }
        }
        public void AddUser(User U)
        {
            Users.Add(U);
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Amir\Documents\Project_AP\OnlineBookStore\DataBase\DatabaseOnlineBookStore.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();
            string command = "Insert into T_User values('"+ U.FirstName +"','"+ U.LastName +"','"+ U.PhoneNumber +"','"+ U.Email +"','"+ U.Password +"','"+ U.AccountBalance +"','"+ U.VIP +"' )";
            SqlCommand com = new SqlCommand(command, connection);
            com.ExecuteNonQuery();
            connection.Close();
        }
        public void EditUser(User NewUser,User OldUser)
        {
            User U = Users.First(x=>x.Email==OldUser.Email);
            int index = Users.IndexOf(U);
            Users[index] = NewUser;
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Amir\Documents\Project_AP\OnlineBookStore\DataBase\DatabaseOnlineBookStore.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();
            string command;
            command = "Update T_User Set FirstName = '" + NewUser.FirstName + "', LastName = '" + NewUser.LastName + "', PhoneNumber = '" + NewUser.PhoneNumber + "', Email = '" + NewUser.Email + "', Password = '" + NewUser.Password + "' Where Email = '" + OldUser.Email + "'; ";
            SqlCommand com = new SqlCommand(command, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            com.ExecuteNonQuery();
            com.Dispose();
            connection.Close();
            OldUser.FirstName = NewUser.FirstName;
            OldUser.LastName = NewUser.LastName;
            OldUser.PhoneNumber = NewUser.PhoneNumber;
            OldUser.Email = NewUser.Email;
            OldUser.Password = NewUser.Password;
        }
        public void EditUser(string Email,double Money)//for change account balance of user in Database
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Amir\Documents\Project_AP\OnlineBookStore\DataBase\DatabaseOnlineBookStore.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();
            string command;
            command = "Update T_User Set AccountBalance = '" + Money + "' Where Email = '" + Email + "'; ";
            SqlCommand com = new SqlCommand(command, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            com.ExecuteNonQuery();
            com.Dispose();
            connection.Close();
        }
        public void EditUser(Book book,User user)//for delete one book of T_BaskeOfGoods and add to T_UsersBooks
        {
            user.BasketOfGoods.Remove(book);
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Amir\Documents\Project_AP\OnlineBookStore\DataBase\DatabaseOnlineBookStore.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();
            string command;
            command = "Delete From T_BasketOfGoods Where ID = '" + book.ID + "' And Email = '" + user.Email + "'; ";
            SqlCommand com = new SqlCommand(command, connection);
            com.ExecuteNonQuery();
            connection.Close();
            user.Books.Add(book);
            SqlConnection connection2 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Amir\Documents\Project_AP\OnlineBookStore\DataBase\DatabaseOnlineBookStore.mdf;Integrated Security=True;Connect Timeout=30");
            connection2.Open();
            command = "Insert into T_UsersBooks values('" + book.ID + "','" + user.Email + "' )";
            SqlCommand com2 = new SqlCommand(command, connection2);
            com2.ExecuteNonQuery();
            connection2.Close();
        }
        public void EditUser(User user)//for delete All book .......
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Amir\Documents\Project_AP\OnlineBookStore\DataBase\DatabaseOnlineBookStore.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();
            string command;
            command = "Delete From T_BasketOfGoods Where Email = '" + user.Email + "'; ";
            SqlCommand com = new SqlCommand(command, connection);
            com.ExecuteNonQuery();
            connection.Close();
            while(user.BasketOfGoods.Count!=0)
            {
                user.Books.Add(user.BasketOfGoods[0]);
                SqlConnection connection2 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Amir\Documents\Project_AP\OnlineBookStore\DataBase\DatabaseOnlineBookStore.mdf;Integrated Security=True;Connect Timeout=30");
                connection2.Open();
                command = "Insert into T_UsersBooks values('" + user.BasketOfGoods[0].ID + "','" + user.Email + "' )";
                SqlCommand com2 = new SqlCommand(command, connection2);
                com2.ExecuteNonQuery();
                connection2.Close();
                user.BasketOfGoods.RemoveAt(0);
            }
        }
        public void DeleteBook_BasketOfGoods(Book book,User user)
        {
            user.BasketOfGoods.Remove(book);
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Amir\Documents\Project_AP\OnlineBookStore\DataBase\DatabaseOnlineBookStore.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();
            string command;
            command = "Delete From T_BasketOfGoods Where ID = '" + book.ID + "' And Email = '" + user.Email + "'; ";
            SqlCommand com = new SqlCommand(command, connection);
            com.ExecuteNonQuery();
            connection.Close();
        }
    }
}
