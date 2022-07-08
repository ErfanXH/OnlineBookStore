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
    public partial class AdminDashboard : Window
    {
        public BookDataAccess bookDataAccess;
        public vipDataAccess vipdataAccess;
        public UserDataAccess userDataAccess;
        public AdminDashboard(UserDataAccess userDataAccess, BookDataAccess bookDataAccess)
        {
            InitializeComponent();

            this.bookDataAccess = bookDataAccess;
            vipdataAccess = new vipDataAccess();
            this.userDataAccess = userDataAccess;
            
            ImportHome();
        }

        private void ImportHome()
        {
            //Home: 
            VIPsCount.Content = userDataAccess.Users.Where(x => x.VIP == true).Count();
            CustomersCount.Content = userDataAccess.Users.Count();
            BooksCount.Content = bookDataAccess.Books.Count();

            //Billing:
            double total = 0;
            foreach (var book in bookDataAccess.Books)
            {
                total += book.AmountOfSales;
            }
            Admin.Balance = total;
            AdminBalance.Content = Admin.Balance;

            //Customers
            DGCustomers.ItemsSource = userDataAccess.Users;
        }

        private void ExitDash_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            Grid_Home.Visibility = Visibility.Visible;
            Grid_Billing.Visibility = Visibility.Collapsed;
            Grid_Books.Visibility = Visibility.Collapsed;
            Grid_Customers.Visibility = Visibility.Collapsed;
            Grid_VIP.Visibility = Visibility.Collapsed;
        }

        private void btnBilling_Click(object sender, RoutedEventArgs e)
        {
            Grid_Home.Visibility = Visibility.Collapsed;
            Grid_Billing.Visibility = Visibility.Visible;
            Grid_Books.Visibility = Visibility.Collapsed;
            Grid_Customers.Visibility = Visibility.Collapsed;
            Grid_VIP.Visibility = Visibility.Collapsed;
        }

        private void btnCustomers_Click(object sender, RoutedEventArgs e)
        {
            Grid_Home.Visibility = Visibility.Collapsed;
            Grid_Billing.Visibility = Visibility.Collapsed;
            Grid_Books.Visibility = Visibility.Collapsed;
            Grid_Customers.Visibility = Visibility.Visible;
            Grid_VIP.Visibility = Visibility.Collapsed;
        }

        private void btnBooks_Click(object sender, RoutedEventArgs e)
        {
            Grid_Home.Visibility = Visibility.Collapsed;
            Grid_Billing.Visibility = Visibility.Collapsed;
            Grid_Books.Visibility = Visibility.Visible;
            Grid_Customers.Visibility = Visibility.Collapsed;
            Grid_VIP.Visibility = Visibility.Collapsed;
        }

        private void btnVIP_Click(object sender, RoutedEventArgs e)
        {
            Grid_Home.Visibility = Visibility.Collapsed;
            Grid_Billing.Visibility = Visibility.Collapsed;
            Grid_Books.Visibility = Visibility.Collapsed;
            Grid_Customers.Visibility = Visibility.Collapsed;
            Grid_VIP.Visibility = Visibility.Visible;
        }

        private void SetVip_Click(object sender, RoutedEventArgs e)
        {
            VipsList vipsList = new VipsList(vipdataAccess);
            vipsList.ShowDialog();
        }

        private void SetVipBook_Click(object sender, RoutedEventArgs e)
        {
            VipBooksList vipbookslist = new VipBooksList(bookDataAccess);
            vipbookslist.ShowDialog();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tbx = sender as TextBox;
            if (tbx.Text != "" && tbx.Text != "Search by First Name, Last Name, Email")
            {
                DGCustomers.ItemsSource = userDataAccess.Users.Where(x => x.FirstName.ToLower().Contains(tbx.Text.ToLower()) || x.LastName.ToLower().Contains(tbx.Text.ToLower()) || x.Email.ToLower().Contains(tbx.Text.ToLower()));
            }
            else if (tbx.Text == "")
            {
                DGCustomers.ItemsSource = userDataAccess.Users.Where(x => x.Email.ToLower() == "");
            }
        }

        private void DGCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGCustomers.SelectedIndex >= 0)
            {
                var user = DGCustomers.SelectedItem as User;
                CustomerInfo customerInfo = new CustomerInfo(userDataAccess, user);
                customerInfo.ShowDialog();
            }
        }

        private void BooksReports_Click(object sender, RoutedEventArgs e)
        {
            BooksReports booksReports = new BooksReports(bookDataAccess, userDataAccess);
            booksReports.ShowDialog();
        }

        private void AED_Click(object sender, RoutedEventArgs e)
        {
            AddEditDeleteBook addEditDeleteBook = new AddEditDeleteBook(bookDataAccess, userDataAccess);
            addEditDeleteBook.ShowDialog();
        }

        private void btnTake_Click(object sender, RoutedEventArgs e)
        {
            Action<string> action = (x => AdminBalance.Content = x);
            Payment payment = new Payment(action, Admin.Balance);
            payment.Show();
        }
    }
}
