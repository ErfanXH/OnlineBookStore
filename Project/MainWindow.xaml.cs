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

namespace HomePage
{
    public enum SearchType
    {
        normal, popularity
    }
    public partial class MainWindow : Window
    {
        BookDataAccess bookdataaccess = new BookDataAccess();

        ObservableCollection<Book> books = new ObservableCollection<Book>();

        Book CurrentBook { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            books = bookdataaccess.Books;
            ListBoxMain.ItemsSource = books.OrderByDescending(x => x.Rating).Take(5);
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
            BookInfo bookInfo = new BookInfo(book, bookdataaccess);
            this.Close();
            bookInfo.ShowDialog();
        }

        private void btnClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void btnCat_Click(object sender, RoutedEventArgs e)
        {
            CatListBooks catlistbooks = new CatListBooks(bookdataaccess);
            catlistbooks.ShowDialog();
        }

        private void btnViewAll_Click(object sender, RoutedEventArgs e)
        {
            ListOfBooks listOfBooks = new ListOfBooks(bookdataaccess, SearchType.normal);
            listOfBooks.ShowDialog();
        }

        private void User_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            /*CustomerDashboard customerDashboard = new CustomerDashboard();
            customerDashboard.ShowDialog();*/
            AdminDashboard adminDashboard = new AdminDashboard();
            adminDashboard.ShowDialog();
        }

        private void ViewPop_Click(object sender, RoutedEventArgs e)
        {
            ListOfBooks listOfBooks = new ListOfBooks(bookdataaccess, SearchType.popularity);
            listOfBooks.ShowDialog();
        }

        private void ListBoxMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listbox = sender as ListBox;
            Book book = (Book)listbox.SelectedItem;
            BookInfo bookInfo = new BookInfo(book, bookdataaccess);
            this.Close();
            bookInfo.ShowDialog();
        }
    }
}
