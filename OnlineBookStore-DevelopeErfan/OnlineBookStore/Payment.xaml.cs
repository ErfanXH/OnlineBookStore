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
using System.Text.RegularExpressions;

namespace OnlineBookStore
{
    enum Operation { ChargeWallet, DeleteOneBook, DeleteAllBook, VIPAccount, Admin }//add Admin
    /// <summary>
    /// Interaction logic for Payment.xaml
    /// </summary>
    public partial class Payment : Window
    {
        User user;
        UserDataAccess userDataAccess;
        Action<string> lbCurrentBalance;
        double ProductPrice;
        Book book;
        Operation Oper;

        public Payment(User user, UserDataAccess userDataAccess, Action<string> lbCurrentBalance)
        {
            InitializeComponent();
            this.user = user;
            this.userDataAccess = userDataAccess;
            this.lbCurrentBalance = lbCurrentBalance;
            Oper = Operation.ChargeWallet;
        }

        public Payment(User user, UserDataAccess userDataAccess, Action<string> lbCurrentBalance, Book book)
        {
            InitializeComponent();
            this.user = user;
            this.userDataAccess = userDataAccess;
            this.lbCurrentBalance = lbCurrentBalance;
            this.book = book;
            Oper = Operation.DeleteOneBook;
            dkPriceOfAllBook.Visibility = Visibility.Visible;
            lbPriceOfAllBook.Content = book.Price.ToString();
        }

        public Payment(User user, UserDataAccess userDataAccess, Action<string> lbCurrentBalance, double Price)
        {
            InitializeComponent();
            this.user = user;
            this.userDataAccess = userDataAccess;
            this.lbCurrentBalance = lbCurrentBalance;
            this.ProductPrice = Price;
            dkPriceOfAllBook.Visibility = Visibility.Visible;
            lbPriceOfAllBook.Content = Price.ToString();
            Oper = Operation.DeleteAllBook;
        }

        public Payment(Action<string> lbCurrentBalance, double Price)//for Admin
        {
            InitializeComponent();
            lbCVV2.Content = "Password: ";//add Name:lbCVV2 in line 17
            this.lbCurrentBalance = lbCurrentBalance;
            this.ProductPrice = Price;
            Oper = Operation.Admin;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            string CartNumber = tbCartNumber.Text;
            int Money;
            if (Regex.IsMatch(CartNumber, "^[0-9]{11}$"))
            {
                int Sum = 0;
                for (int i = 0; i < 11; i++)
                {
                    int n = int.Parse(CartNumber[i].ToString());
                    if (i % 2 == 1)
                    {
                        n *= 2;
                        n = n % 10 + n / 10;
                    }
                    Sum += n;
                }
                if (Sum % 10 != 0)
                {
                    MessageBox.Show("The card number isn't correct!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("The card number isn't correct!");
                return;
            }
            if (Oper == Operation.Admin)//add line
            {
                if (tbCVV2.Text != Admin.Password)
                {
                    MessageBox.Show("The Password isn't correct!");
                    return;
                }
            }
            else
            {
                if (!Regex.IsMatch(tbCVV2.Text, "^[1-9][0-9]{2,3}$"))
                {
                    MessageBox.Show("The CVV2 isn't correct!");
                    return;
                }
            }
            if (tbMoney.Text == "")
            {
                MessageBox.Show("The money isn't correct!");
                return;
            }
            if (tbMoney.Text[0] != '0')
            {
                try
                {
                    Money = int.Parse(tbMoney.Text);
                }
                catch
                {
                    MessageBox.Show("The money isn't correct!Please input integer.");
                    return;
                }
                if (Money < 0)
                {
                    MessageBox.Show("The money isn't correct!Please input positive number.");
                    return;
                }
            }
            else
            {
                MessageBox.Show("The money isn't correct!Please input integer.");
                return;
            }
            if (Oper == Operation.ChargeWallet)
            {
                user.AccountBalance += Money;
                userDataAccess.EditUser(user.Email, user.AccountBalance);
                lbCurrentBalance(user.AccountBalance.ToString());
            }
            else
            {
                if (Oper == Operation.DeleteOneBook)
                {
                    if (Money < book.Price)
                    {
                        MessageBox.Show("The price of book is " + book.Price + "!");
                        return;
                    }
                    else
                    {
                        user.AccountBalance += Money - book.Price;
                        userDataAccess.EditUser(user.Email, user.AccountBalance);
                        lbCurrentBalance(user.AccountBalance.ToString());
                        userDataAccess.EditUser(book, user);
                    }
                }
                else
                {
                    if (Oper == Operation.DeleteAllBook)
                    {
                        if (Money < ProductPrice)
                        {
                            MessageBox.Show("The price of books is " + ProductPrice + "!");
                            return;
                        }
                        else
                        {
                            user.AccountBalance += Money - ProductPrice;
                            userDataAccess.EditUser(user.Email, user.AccountBalance);
                            lbCurrentBalance(user.AccountBalance.ToString());
                            userDataAccess.EditUser(user);
                        }
                    }
                    else//Add
                    {
                        if (Oper == Operation.Admin)
                        {
                            if (Money > ProductPrice)
                            {
                                MessageBox.Show("The Balance of store is " + ProductPrice + "!");
                                return;
                            }
                            else
                            {
                                Admin.Balance -= Money;
                                lbCurrentBalance(Admin.Balance.ToString());
                            }
                        }
                    }
                }
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}