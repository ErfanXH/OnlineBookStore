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

namespace HomePage
{
    public partial class ListOfBooks : Window
    {
        ObservableCollection<Book> books = new ObservableCollection<Book>();
        BookDataAccess bookDataAccess;

        public ListOfBooks(BookDataAccess bookDataAccess, SearchType searchType)
        {
            InitializeComponent();
            this.bookDataAccess = bookDataAccess;
            books = bookDataAccess.Books;
            if (searchType == SearchType.normal)
                ListBox1.ItemsSource = books;
            else if (searchType == SearchType.popularity)
                ListBox1.ItemsSource = books.OrderByDescending(x => x.Rating);
        }

        private void User_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CustomerDashboard customerDashboard = new CustomerDashboard();
            customerDashboard.ShowDialog();
        }

        private void btnClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.ShowDialog();
        }

        private void ListBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listbox = sender as ListBox;
            Book book = (Book)listbox.SelectedItem;
            BookInfo bookInfo = new BookInfo(book, bookDataAccess);
            bookInfo.ShowDialog();
            this.Close();
        }
    }
}
