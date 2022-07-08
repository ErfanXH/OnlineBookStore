using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class ListOfBooks : Window
    {
        ObservableCollection<Book> books;
        BookDataAccess bookDataAccess;
        User user;
        UserDataAccess userDataAccess;
        public ListOfBooks(BookDataAccess bookDataAccess, SearchType searchType,User user,UserDataAccess userDataAccess)
        {
            InitializeComponent();
            this.bookDataAccess = bookDataAccess;
            books = bookDataAccess.Books;

            if (searchType == SearchType.normal)
                ListBox1.ItemsSource = books;
            else if (searchType == SearchType.popularity)
                ListBox1.ItemsSource = books.Where(x => x.Rating >= 5).OrderByDescending(x => x.Rating);
            else if (searchType == SearchType.time)
                ListBox1.ItemsSource = books.Where(x => x.ReleaseTime >= 2016).OrderByDescending(x => x.ReleaseTime);
            else if (searchType == SearchType.vip)
                ListBox1.ItemsSource = books.Where(x => x.booktype == BookType.VIP);

            this.user = user;
            this.userDataAccess = userDataAccess;
            UserName.Content = user.FirstName;
        }

        private void User_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CustomerDashboard customerDashboard = new CustomerDashboard(user,userDataAccess);
            customerDashboard.ShowDialog();
        }

        private void btnClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            HomePage homePage = new HomePage(user, userDataAccess, bookDataAccess);
            this.Close();
            homePage.ShowDialog();
        }

        private void ListBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listbox = sender as ListBox;
            Book book = (Book)listbox.SelectedItem;
            BookInfo bookInfo = new BookInfo(book, bookDataAccess,user,userDataAccess);
            this.Close();
            bookInfo.ShowDialog();
            //this.Close();
        }
    }
}
