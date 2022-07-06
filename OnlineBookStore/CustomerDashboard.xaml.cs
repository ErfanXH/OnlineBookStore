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
    /// <summary>
    /// Interaction logic for CustomerDashboard.xaml
    /// </summary>
    public partial class CustomerDashboard : Window
    {
        User user;
        UserDataAccess userDataAccess;
        public CustomerDashboard(User user,UserDataAccess userDataAccess)
        {
            InitializeComponent();
            this.user = user;
            this.userDataAccess = userDataAccess;
            lbCustomerNameBilling.Content = user.FirstName + " " + user.LastName;
            lbCustomerBalanceBilling.Content = user.AccountBalance;
        }

        private void ExitDash_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            Grid_Home.Visibility = Visibility.Visible;
            Grid_Billing.Visibility = Visibility.Collapsed;
            Grid_VIP.Visibility = Visibility.Collapsed;
            Grid_Cart.Visibility = Visibility.Collapsed;
        }

        private void btnBilling_Click(object sender, RoutedEventArgs e)
        {
            Grid_Home.Visibility = Visibility.Collapsed;
            Grid_Billing.Visibility = Visibility.Visible;
            Grid_VIP.Visibility = Visibility.Collapsed;
            Grid_Cart.Visibility = Visibility.Collapsed;
        }

        private void btnVIP_Click(object sender, RoutedEventArgs e)
        {
            Grid_Home.Visibility = Visibility.Collapsed;
            Grid_Billing.Visibility = Visibility.Collapsed;
            Grid_VIP.Visibility = Visibility.Visible;
            Grid_Cart.Visibility = Visibility.Collapsed;
        }

        private void btnCart_Click(object sender, RoutedEventArgs e)
        {
            Grid_Home.Visibility = Visibility.Collapsed;
            Grid_Billing.Visibility = Visibility.Collapsed;
            Grid_VIP.Visibility = Visibility.Collapsed;
            Grid_Cart.Visibility = Visibility.Visible;
        }

        private void btnEditProfile_Click(object sender, RoutedEventArgs e)
        {
            EditProfileUser E = new EditProfileUser(user, userDataAccess);
            E.Show();
        }

        private void btnChargeCartBilling_Click(object sender, RoutedEventArgs e)
        {
            Action<string> lbCurrentBalance = x => lbCustomerBalanceBilling.Content = x;
            Payment P = new Payment(user, userDataAccess,lbCurrentBalance);
            P.Show();
        }

        private void btnBasketOfGoodsAndTransactions_Click(object sender, RoutedEventArgs e)
        {
            Action<string> lbCurrentBalance = x => lbCustomerBalanceBilling.Content = x;
            UserProducts userProducts = new UserProducts(user, userDataAccess, lbCurrentBalance);
            userProducts.Show();
        }
    }
}
