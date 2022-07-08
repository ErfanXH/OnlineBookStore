using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using DataAccess;
using DataAccess.Models;

namespace OnlineBookStore
{
    public enum SearchType
    {
        normal, popularity, time, vip
    }
    public partial class HomePage : Window
    {
        BookDataAccess bookdataaccess;

        ObservableCollection<Book> books;

        Book CurrentBook { get; set; }
        User user;
        UserDataAccess userDataAccess;
        public HomePage(User user,UserDataAccess userDataAccess, BookDataAccess bookDataAccess)
        {
            InitializeComponent();
            this.bookdataaccess = bookDataAccess;
            books = bookdataaccess.Books;
            ListBoxMain.ItemsSource = books.OrderByDescending(x => x.Rating).Take(5);
            this.user = user;
            this.userDataAccess = userDataAccess;
            UserName.Content = user.FirstName;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tbx = sender as TextBox;
            if (tbx.Text != "" && tbx.Text != "Search by Author, Title")
            {
                ListBoxSearch.ItemsSource = books.Where(x => x.Title.ToLower().Contains(tbx.Text.ToLower()) || x.author.FullName.ToLower().Contains(tbx.Text.ToLower()));
            }
            else if (tbx.Text == "")
            {
                ListBoxSearch.ItemsSource = books.Where(x => x.Title.ToLower() == "");
            }
        }

        private void ListBoxSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listbox = sender as ListBox;
            Book book = (Book)listbox.SelectedItem;
            BookInfo bookInfo = new BookInfo(book, bookdataaccess,user,userDataAccess);
            this.Close();
            bookInfo.ShowDialog();
        }

        private void btnClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void btnCat_Click(object sender, RoutedEventArgs e)
        {
            ListOfBooks listOfBooks = new ListOfBooks(bookdataaccess, SearchType.time, user, userDataAccess);
            this.Close();
            listOfBooks.ShowDialog();
        }

        private void btnVIP_Click(object sender, RoutedEventArgs e)
        {
            if (user.VIP)
            {
                ListOfBooks listOfBooks = new ListOfBooks(bookdataaccess, SearchType.vip, user, userDataAccess);
                this.Close();
                listOfBooks.ShowDialog();
            }
        }

        private void btnViewAll_Click(object sender, RoutedEventArgs e)
        {
            ListOfBooks listOfBooks = new ListOfBooks(bookdataaccess, SearchType.normal,user,userDataAccess);
            this.Close();
            listOfBooks.ShowDialog();
        }

        private void User_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CustomerDashboard customerDashboard = new CustomerDashboard(user,userDataAccess);
            customerDashboard.ShowDialog();
        }

        private void ViewPop_Click(object sender, RoutedEventArgs e)
        {
            ListOfBooks listOfBooks = new ListOfBooks(bookdataaccess, SearchType.popularity,user,userDataAccess);
            this.Close();
            listOfBooks.ShowDialog();
        }

        private void ListBoxMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listbox = sender as ListBox;
            Book book = (Book)listbox.SelectedItem;
            BookInfo bookInfo = new BookInfo(book, bookdataaccess,user,userDataAccess);
            this.Close();
            bookInfo.ShowDialog();
        }

    }
}
