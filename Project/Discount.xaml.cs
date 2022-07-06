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

namespace HomePage
{
    public partial class Discount : Window
    {
        BookDataAccess bookDataAccess = new BookDataAccess();
        Book CurrentBook { get; set; }
        public Discount(Book book, BookDataAccess bookData)
        {
            InitializeComponent();
            CurrentBook = book;
            bookDataAccess = bookData;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            double percentage = double.Parse(TextBox.Text.ToString());
            int time = int.Parse(TextBoxTime.Text.ToString());

            if (percentage > 100 || percentage <= 0 || time <= 0)
                this.Close();
            else
            {
                bookDataAccess.SetDiscount(percentage, CurrentBook, time);

                int index = bookDataAccess.Books.IndexOf(CurrentBook);
                bookDataAccess.Books[index].CurrentPrice = ((100 - percentage) * bookDataAccess.Books[index].MainPrice) / 100;
            }
            this.Close();
        }
    }
}
