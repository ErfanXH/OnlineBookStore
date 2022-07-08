using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
            this.userDataAccess = userDataAccess;
            this.user = user;
            UserName.Content = user.FirstName;
            ImportBook();
            ListBoxBooks.ItemsSource = bookdataAccess.Books.Where(x => x.category == CurrentBook.category && x.ID != CurrentBook.ID).Take(5);
            ImageAddress = CurrentBook.ImageAddress;
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
            Time.Content = CurrentBook.ReleaseTime;

            if (CurrentBook.booktype.ToString() != "VIP")
                isVIP.Visibility = Visibility.Hidden;

            if (user.BookMark.Any(x => x.ID == CurrentBook.ID))
            {
                Mark.Visibility = Visibility.Collapsed;
                UnMark.Visibility = Visibility.Visible;
            }

            if (user.Books.Any(x => x.ID == CurrentBook.ID))
            {
                Buy.Visibility = Visibility.Collapsed;
                Read.Visibility = Visibility.Visible;
                RateBook.Visibility = Visibility.Visible;
            }

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
            HomePage homePage = new HomePage(user, userDataAccess, bookdataAccess);
            this.Close();
            homePage.ShowDialog();
        }

        private void Mark_Click(object sender, RoutedEventArgs e)
        {
            Mark.Visibility = Visibility.Collapsed;
            UnMark.Visibility = Visibility.Visible;

            userDataAccess.AddBookMark(CurrentBook, user);
            //user.BookMark.Add(CurrentBook);
        }

        private void UnMark_Click(object sender, RoutedEventArgs e)
        {
            UnMark.Visibility = Visibility.Collapsed;
            Mark.Visibility = Visibility.Visible;

            userDataAccess.RemoveBookMark(CurrentBook, user);
            //user.BookMark.Remove(CurrentBook);
        }

        private void RateBook_Click(object sender, RoutedEventArgs e)
        {
            RateBook button = new RateBook(CurrentBook, user, bookdataAccess);
            button.ShowDialog();
        }

        private void Comment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            if (!user.BasketOfGoods.Any(x => x.ID == CurrentBook.ID))
                userDataAccess.AddBook_BasketOfGoods(CurrentBook, user);

            //user.BasketOfGoods.Add(CurrentBook);
        }

        private void Read_Click(object sender, RoutedEventArgs e)
        {
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo()
            {
                CreateNoWindow = false,
                FileName = @"C:\Program Files (x86)\Adobe\Acrobat Reader DC\Reader\AcroRd32.exe",
                Arguments = $"{CurrentBook.PDFAddress}"
                // Arguments = @"C:\Users\lenovo\Documents\APProject.pdf"
                // Arguments = @"Pictures/Project.pdf"
            };
            p.Start();
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