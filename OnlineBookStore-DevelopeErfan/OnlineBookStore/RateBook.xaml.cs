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
    public partial class RateBook : Window
    {
        Book book;
        User user;
        BookDataAccess bookDataAccess;
        public RateBook(Book book, User user, BookDataAccess bookDataAccess)
        {
            InitializeComponent();
            this.book = book;
            this.user = user;
            this.bookDataAccess = bookDataAccess;
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            if (combo.SelectedIndex >= 0)
            {
                int newRate = combo.SelectedIndex + 1;
                bookDataAccess.AddRatingBook(book, user, newRate);
            }
            this.Close();
        }
    }
}
