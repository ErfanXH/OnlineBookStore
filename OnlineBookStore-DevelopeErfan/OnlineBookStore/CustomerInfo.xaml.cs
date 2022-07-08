using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataAccess;
using DataAccess.Models;

namespace OnlineBookStore
{
    public partial class CustomerInfo : Window
    {
        UserDataAccess userDataAccess;
        User CurrentUser { get; set; }
        public CustomerInfo(UserDataAccess userDataAccesss, User user)
        {
            InitializeComponent();
            userDataAccess = userDataAccesss;
            CurrentUser = user;
            List<User> users = new List<User>(); users.Add(CurrentUser);
            ListBoxCustomer.ItemsSource = users;

            ListBoxBooks.ItemsSource = CurrentUser.Books;
        }

        private void ExitDash_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
