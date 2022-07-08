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
using DataAccess.Models;
using DataAccess;

namespace OnlineBookStore
{
    /// <summary>
    /// Interaction logic for UserProducts.xaml
    /// </summary>
    public partial class UserProducts : Window
    {
        User user;
        UserDataAccess userDataAccess;
        Book CurrentBook;
        Action<string> lbCurrentBalance;
        public UserProducts(User user, UserDataAccess userDataAccess, Action<string> lbCurrentBalance)
        {
            InitializeComponent();
            BasketOfGoodsGrid.ItemsSource = user.BasketOfGoods;
            TransactionsGrid.ItemsSource = user.Books;
            this.user = user;
            this.userDataAccess = userDataAccess;
            this.lbCurrentBalance = lbCurrentBalance;
        }

        private void BasketOfGoodsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BasketOfGoodsGrid.SelectedIndex >= 0)
            {
                CurrentBook = BasketOfGoodsGrid.SelectedItem as Book;
                lbDescriptionBasket.Content = CurrentBook.Description;
            }
        }

        private void btnBuyOneByCard_Click(object sender, RoutedEventArgs e)
        {
            if (BasketOfGoodsGrid.SelectedIndex >= 0)
            {
                CurrentBook = BasketOfGoodsGrid.SelectedItem as Book;
                Payment P = new Payment(user, userDataAccess, lbCurrentBalance,CurrentBook);
                P.Show();
            }
        }

        private void btnBuyOneByWallet_Click(object sender, RoutedEventArgs e)
        {
            if (BasketOfGoodsGrid.SelectedIndex >= 0)
            {
                CurrentBook = BasketOfGoodsGrid.SelectedItem as Book;
                if (user.AccountBalance < CurrentBook.Price)
                {
                    MessageBox.Show("your account balance is " + user.AccountBalance + "!");
                    return;
                }
                else
                {
                    user.AccountBalance -= CurrentBook.Price;
                    userDataAccess.EditUser(user.Email, user.AccountBalance);
                    userDataAccess.EditUser(CurrentBook, user);
                    lbCurrentBalance(user.AccountBalance.ToString());
                }
            }
        }

        private void btnBuyAllByCard_Click(object sender, RoutedEventArgs e)
        {
            double price = 0;
            for(int i = 0; i < user.BasketOfGoods.Count; i++)
            {
                price += user.BasketOfGoods[i].Price;
            }
            if (price == 0)
            {
                return;
            }
            Payment P = new Payment(user, userDataAccess, lbCurrentBalance, price);
            P.Show();
        }

        private void btnBuyAllByWallet_Click(object sender, RoutedEventArgs e)
        {
            double price = 0;
            for (int i = 0; i < user.BasketOfGoods.Count; i++)
            {
                price += user.BasketOfGoods[i].Price;
            }
            if (user.AccountBalance < price)
            {
                MessageBox.Show("your account balance is " + user.AccountBalance + "!"+"The price of books is "+price+".");
                return;
            }
            else
            {
                user.AccountBalance -= price;
                userDataAccess.EditUser(user.Email, user.AccountBalance);
                userDataAccess.EditUser(user);
                lbCurrentBalance(user.AccountBalance.ToString());
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (BasketOfGoodsGrid.SelectedIndex >= 0)
            {
                CurrentBook = BasketOfGoodsGrid.SelectedItem as Book;
                userDataAccess.DeleteBook_BasketOfGoods(CurrentBook, user);
            }
        }

        private void btnBasket_Click(object sender, RoutedEventArgs e)
        {
            BasketOfGoodsPanel.Visibility = Visibility.Visible;
            TransactionsPanel.Visibility = Visibility.Collapsed;
            HomePanel.Visibility = Visibility.Collapsed;
            if (user.BasketOfGoods.Count == 0)
            {
                lbDescriptionBasket.Content = "---";
            }
        }

        private void btnTransactions_Click(object sender, RoutedEventArgs e)
        {
            BasketOfGoodsPanel.Visibility = Visibility.Collapsed;
            TransactionsPanel.Visibility = Visibility.Visible;
            HomePanel.Visibility = Visibility.Collapsed;
            double price = 0;
            for(int i = 0; i < user.Books.Count; i++)
            {
                price += user.Books[i].Price;
            }
            lbTotalPrice.Content = "Total Price: " + price;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
