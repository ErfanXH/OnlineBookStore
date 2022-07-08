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

namespace OnlineBookStore
{
    public partial class BooksReports : Window
    {
        BookDataAccess bookDataAccess;
        UserDataAccess userDataAccess;
        public BooksReports(BookDataAccess bookData, UserDataAccess userData)
        {
            InitializeComponent();
            bookDataAccess = bookData;
            userDataAccess = userData;

            foreach (var book in bookDataAccess.Books)
                book.NumberOfSales = 0;

            // Setting Number Of Sales
            for (int i = 0; i < bookDataAccess.Books.Count; i++)
                for (int j = 0; j < userDataAccess.Users.Count; j++)
                    for (int k = 0; k < userDataAccess.Users[j].Books.Count; k++)
                        if (bookDataAccess.Books[i].ID == userDataAccess.Users[j].Books[k].ID)
                            bookDataAccess.Books[i].NumberOfSales++;

            ListBoxBooks.ItemsSource = bookDataAccess.Books;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tbx = sender as TextBox;
            if (tbx.Text != "" && tbx.Text != "Search by Author, Title")
            {
                ListBoxBooks.ItemsSource = bookDataAccess.Books.Where(x => x.Title.ToLower().Contains(tbx.Text.ToLower()) || x.author.FullName.ToLower().Contains(tbx.Text.ToLower()));
            }
            else if (tbx.Text == "")
            {
                ListBoxBooks.ItemsSource = bookDataAccess.Books;
            }
        }

        private void ListBoxBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listbox = sender as ListBox;
            Book book = (Book)listbox.SelectedItem;
            Discount discount = new Discount(book, bookDataAccess);
            discount.ShowDialog();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
