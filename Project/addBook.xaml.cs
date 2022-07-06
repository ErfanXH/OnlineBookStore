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
    public partial class addBook : Window
    {
        BookDataAccess bookDataAccess { get; set; }
        public addBook(BookDataAccess bookData)
        {
            InitializeComponent();
            bookDataAccess = bookData;
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            Book newBook = new Book(title.Text, desc.Text, int.Parse(page.Text), DateTime.Now, 0, double.Parse(price.Text), image.Text, new Author(author.Text), (BookType)Enum.Parse(typeof(BookType), type.Text), (Category)Enum.Parse(typeof(Category), category.Text), double.Parse(price.Text));

            bookDataAccess.AddBook(newBook);
            this.Close();
        }
    }
}
