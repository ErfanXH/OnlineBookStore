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
    public partial class VipBooksList : Window
    {
        BookDataAccess bookDataAccess = new BookDataAccess();
        public VipBooksList(BookDataAccess bookDataAccess)
        {
            InitializeComponent();
            this.bookDataAccess = bookDataAccess;
            // ListBoxBooks.ItemsSource = bookDataAccess.Books;
            DGBooks.ItemsSource = bookDataAccess.Books;
        }

        private void VIP_Click(object sender, RoutedEventArgs e)
        {
            if (DGBooks.SelectedIndex >= 0)
            {
                Book selectedBook = DGBooks.SelectedItem as Book;

                bookDataAccess.ChangeBookType(selectedBook, BookType.VIP);
            }
        }

        private void Ord_Click(object sender, RoutedEventArgs e)
        {
            if (DGBooks.SelectedIndex >= 0)
            {
                Book selectedBook = DGBooks.SelectedItem as Book;

                bookDataAccess.ChangeBookType(selectedBook, BookType.Ordinary);
            }
        }
    }
}
