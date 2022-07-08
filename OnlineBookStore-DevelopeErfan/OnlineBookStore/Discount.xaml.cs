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
using System.Text.RegularExpressions;

namespace OnlineBookStore
{
    public partial class Discount : Window
    {
        BookDataAccess bookDataAccess;
        Book CurrentBook { get; set; }
        public Discount(Book book, BookDataAccess bookData)
        {
            InitializeComponent();
            CurrentBook = book;
            bookDataAccess = bookData;
            ID.Content = CurrentBook.ID;
            Price.Content = CurrentBook.MainPrice;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            double percentage;
            try
            {
                percentage = double.Parse(TextBox.Text.ToString());
            }
            catch
            {
                MessageBox.Show("The Percentage isn't correct!");
                return;
            }
            if (percentage <= 0 || percentage>100)
            {
                MessageBox.Show("The Percentage isn't correct!");
                return;
            }
            if (!Regex.IsMatch(TextBoxTime.Text, "^[1-9][0-9]$"))
            {
                MessageBox.Show("The Time isn't correct!");
                return;
            }
            int time = int.Parse(TextBoxTime.Text.ToString());

            
            bookDataAccess.SetDiscount(percentage, CurrentBook, time);
            int index = bookDataAccess.Books.IndexOf(CurrentBook);
            bookDataAccess.Books[index].Price = ((100 - percentage) * bookDataAccess.Books[index].MainPrice) / 100;
            
            this.Close();
        }
    }
}
