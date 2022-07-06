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
    /// <summary>
    /// Interaction logic for EditProfileUser.xaml
    /// </summary>
    public partial class EditProfileUser : Window
    {
        User user;
        UserDataAccess userDataAccess;
        public EditProfileUser(User user,UserDataAccess userDataAccess)
        {
            InitializeComponent();
            this.user = user;
            this.userDataAccess = userDataAccess;
            tbFirstName.Text = user.FirstName;
            tbLastName.Text = user.LastName;
            tbPhoneNumber.Text = user.PhoneNumber;
            tbEmail.Text = user.Email;
            tbPassword.Text = user.Password;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (user.FirstName == tbFirstName.Text && user.LastName == tbLastName.Text && user.PhoneNumber == tbPhoneNumber.Text && user.Email == tbEmail.Text && user.Password == tbPassword.Text)
            {
                MessageBox.Show("Nothing has changed!");
                return;
            }
            string firstName = tbFirstName.Text;
            string lastName = tbLastName.Text;
            string phoneNumber = tbPhoneNumber.Text;
            string email = tbEmail.Text;
            string password = tbPassword.Text;
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
            if (email != user.Email)
            {
                if (userDataAccess.Users.Any(x => x.Email == email))
                {
                    MessageBox.Show("The email is duplicated!");
                    return;
                }
            }
            if (!(Regex.IsMatch(password, "^.{8,40}$") && Regex.IsMatch(password, "[a-z]{1,}") && Regex.IsMatch(password, "[A-Z]{1,}")))
            {
                MessageBox.Show("The password isn't correct!");
                return;
            }
            User NewUser = new User()
            {
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                Email = email,
                Password = password
            };
            userDataAccess.EditUser(NewUser, user);
            this.Close();
        }
    }
}
