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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using DataAccess;
using DataAccess.Models;

namespace OnlineBookStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserDataAccess userDataAccess;
        AdminDataAccess adminDataAccess;
        public MainWindow()
        {
            InitializeComponent();
            userDataAccess = new UserDataAccess();
            adminDataAccess = new AdminDataAccess();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            HomePanel.Visibility = Visibility.Collapsed;
            UserLoginPanle.Visibility = Visibility.Visible;
            RegisterUserPanel.Visibility = Visibility.Collapsed;
            AdminLoginPanel.Visibility = Visibility.Collapsed;
            SuccessfullyPanel.Visibility = Visibility.Collapsed;
            tbFirstNameRegister.Text = null;
            tbLastNameRegister.Text = null;
            tbPhoneNumberRegister.Text = null;
            tbEmailRegister.Text = null;
            pbPasswordRegister.Password = null;
            tbUsernameAdmin.Text = null;
            pbPasswordAdmin.Password = null;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            HomePanel.Visibility = Visibility.Collapsed;
            UserLoginPanle.Visibility = Visibility.Collapsed;
            RegisterUserPanel.Visibility = Visibility.Visible;
            AdminLoginPanel.Visibility = Visibility.Collapsed;
            SuccessfullyPanel.Visibility = Visibility.Collapsed;
            tbEmailUserLogin.Text = null;
            pbPasswordUserLogin.Password = null;
            tbUsernameAdmin.Text = null;
            pbPasswordAdmin.Password = null;
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            HomePanel.Visibility = Visibility.Collapsed;
            UserLoginPanle.Visibility = Visibility.Collapsed;
            RegisterUserPanel.Visibility = Visibility.Collapsed;
            AdminLoginPanel.Visibility = Visibility.Visible;
            SuccessfullyPanel.Visibility = Visibility.Collapsed;
            tbEmailUserLogin.Text = null;
            pbPasswordUserLogin.Password = null;
            tbFirstNameRegister.Text = null;
            tbLastNameRegister.Text = null;
            tbPhoneNumberRegister.Text = null;
            tbEmailRegister.Text = null;
            pbPasswordRegister.Password = null;
        }

        private void btnUserRegister_Click(object sender, RoutedEventArgs e)
        {
            string firstName = tbFirstNameRegister.Text;
            string lastName = tbLastNameRegister.Text;
            string phoneNumber = tbPhoneNumberRegister.Text;
            string email = tbEmailRegister.Text;
            string password = pbPasswordRegister.Password;
            if (!Regex.IsMatch(firstName, "^[a-zA-Z]{3,32}$"))
            {
                MessageBox.Show("The first name isn't correct!");
                return;
            }
            if (!Regex.IsMatch(lastName, "^[a-zA-Z]{3,32}$"))
            {
                MessageBox.Show("The last name isn't correct!");
                return;
            }
            if (!Regex.IsMatch(phoneNumber, "^09[0-9]{9}$"))
            {
                MessageBox.Show("The phone number isn't correct!");
                return;
            }
            if (!Regex.IsMatch(email, "^.{0,32}@.{0,32}[.].{0,32}"))
            {
                MessageBox.Show("The email isn't correct!");
                return;
            }
            if (userDataAccess.Users.Any(x => x.Email == email))
            {
                MessageBox.Show("The email is duplicated!");
                return;
            }
            if (!(Regex.IsMatch(password,"^.{8,40}$") && Regex.IsMatch(password,"[a-z]{1,}") && Regex.IsMatch(password,"[A-Z]{1,}")))
            {
                MessageBox.Show("The password isn't correct!");
                return;
            }
            User U = new User()
            {
                FirtName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                Email = email,
                Password = password
            };
            userDataAccess.AddUser(U);
            HomePanel.Visibility = Visibility.Collapsed;
            UserLoginPanle.Visibility = Visibility.Collapsed;
            RegisterUserPanel.Visibility = Visibility.Collapsed;
            AdminLoginPanel.Visibility = Visibility.Collapsed;
            SuccessfullyPanel.Visibility = Visibility.Visible;
            tbFirstNameRegister.Text = null;
            tbLastNameRegister.Text = null;
            tbPhoneNumberRegister.Text = null;
            tbEmailRegister.Text = null;
            pbPasswordRegister.Password = null;
        }

        private void btnLoginUserLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = tbEmailUserLogin.Text;
            string password = pbPasswordUserLogin.Password;
            User U;
            try
            {
                U = userDataAccess.Users.First(x => x.Email == email && x.Password == password);
            }
            catch
            {
                MessageBox.Show("The information is invalid!");
                return;
            }
            //go to home panel User U
        }

        private void btnLoginAdmin_Click(object sender, RoutedEventArgs e)
        {
            string username = tbUsernameAdmin.Text;
            string password = pbPasswordAdmin.Password;
            Admin A;
            if (Regex.IsMatch(username, "^[a-zA-Z]{3,32}$"))
            {
                if (password == Admin.Password)
                {
                    if (adminDataAccess.Admins.Any(x => x.Username == username))
                    {
                        A = adminDataAccess.Admins.First(x => x.Username == username);
                    }
                    else
                    {
                        A = new Admin() { Username = username };
                        adminDataAccess.AddAdmin(A);
                    }
                    //go to home panel Admin A
                }
                else
                {
                    MessageBox.Show("The password isn't correct!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("The username isn't correct!");
                return;
            }
        }
    }
}
