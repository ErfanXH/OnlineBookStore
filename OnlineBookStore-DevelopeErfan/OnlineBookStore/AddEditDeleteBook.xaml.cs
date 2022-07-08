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
    public partial class AddEditDeleteBook : Window
    {
        BookDataAccess bookDataAccess;
        UserDataAccess userDataAccess;
        public AddEditDeleteBook(BookDataAccess bookData, UserDataAccess userData)
        {
            InitializeComponent();
            bookDataAccess = bookData;
            userDataAccess = userData;

            // Setting Number Of Sales
            for (int i = 0; i < bookDataAccess.Books.Count; i++)
                for (int j = 0; j < userDataAccess.Users.Count; j++)
                    for (int k = 0; k < userDataAccess.Users[j].Books.Count; k++)
                        if (bookDataAccess.Books[i].ID == userDataAccess.Users[j].Books[k].ID)
                            bookDataAccess.Books[i].NumberOfSales++;

            ListBoxBooks.ItemsSource = bookDataAccess.Books;
        }

        private void ListBoxBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listbox = sender as ListBox;
            Book book = (Book)listbox.SelectedItem;


        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            addBook addBook = new addBook(bookDataAccess);
            addBook.ShowDialog();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxBooks.SelectedIndex >= 0)
            {
                var book = ListBoxBooks.SelectedItem as Book;
                EditBook editBook = new EditBook(bookDataAccess, book);
                editBook.ShowDialog();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxBooks.SelectedIndex >= 0)
            {
                var book = ListBoxBooks.SelectedItem as Book;
                bookDataAccess.RemoveBook(book);
            }
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
