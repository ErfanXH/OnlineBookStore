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
        BookDataAccess bookDataAccess = new BookDataAccess();
        UserDataAccess userDataAccess = new UserDataAccess();
        public BooksReports(BookDataAccess bookData, UserDataAccess userData)
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
            Discount discount = new Discount(book, bookDataAccess);
            discount.ShowDialog();
        }
    }
}
