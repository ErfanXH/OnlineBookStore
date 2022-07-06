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
    public partial class BookInfo : Window
    {
        Book CurrentBook { set; get; }
        BookDataAccess bookdataAccess { set; get; }
        public string ImageAddress { get; set; }
        User user;
        UserDataAccess userDataAccess;
        public BookInfo(Book book, BookDataAccess bookData,User user,UserDataAccess userDataAccess)
        {
            InitializeComponent();
            CurrentBook = book;
            bookdataAccess = bookData;
            ImportBook();
            ListBoxBooks.ItemsSource = bookdataAccess.Books.Where(x => x.category == CurrentBook.category && x.ID != CurrentBook.ID).Take(5);
            // ListBoxThisBook.ItemsSource = bookdataAccess.Books.Where(x => x.ID == CurrentBook.ID);
            ImageAddress = CurrentBook.ImageAddress;
            this.user = user;
            this.userDataAccess = userDataAccess;
        }
        public void ImportBook()
        {
            BookTitle.Content = CurrentBook.Title;
            BookAuthor.Content = CurrentBook.author.FullName;
            BookDesc.Content = CurrentBook.Description;
            BookPrice.Content = "Price: " + CurrentBook.Price.ToString();   //CurrentPrice !!!!!
            BookRating.Content = "Rating: " + CurrentBook.Rating + "/10";
            Category.Content = CurrentBook.category.ToString();
            Page.Content = CurrentBook.NumOfPages;
            Time.Content = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString();

            // imgThisBook.Source = CurrentBook.ImageAddress;

            if (CurrentBook.booktype.ToString() != "VIP")
                isVIP.Visibility = Visibility.Hidden;

        }

        private void ImgUser_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.ShowDialog();
        }

        private void Mark_Click(object sender, RoutedEventArgs e)
        {
            Mark.Visibility = Visibility.Collapsed;
            UnMark.Visibility = Visibility.Visible;
        }

        private void UnMark_Click(object sender, RoutedEventArgs e)
        {
            UnMark.Visibility = Visibility.Collapsed;
            Mark.Visibility = Visibility.Visible;
        }

        private void RateBook_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Comment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Read_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ReadSample_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ViewCat_Click(object sender, RoutedEventArgs e)
        {
            BookDataAccess tmp = new BookDataAccess();
            tmp.Books.Clear();

            foreach (var item in bookdataAccess.Books)
            {
                if (item.category == CurrentBook.category)
                    tmp.Books.Add(item);
            }

            ListOfBooks listofbooks = new ListOfBooks(tmp, SearchType.normal,user,userDataAccess);
            this.Close();
            listofbooks.ShowDialog();
        }

        private void ListBoxBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listbox = sender as ListBox;
            Book book = (Book)listbox.SelectedItem;
            BookInfo bookInfo = new BookInfo(book, bookdataAccess,user,userDataAccess);
            this.Close();
            bookInfo.ShowDialog();
        }
    }
}