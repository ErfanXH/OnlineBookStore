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
    public partial class EditBook : Window
    {
        BookDataAccess bookDataAccess { get; set; }
        Book CurrentBook { get; set; }
        public EditBook(BookDataAccess bookData, Book book)
        {
            InitializeComponent();
            bookDataAccess = bookData;
            CurrentBook = book;

            title.Text = CurrentBook.Title;
            desc.Text = CurrentBook.Description;
            page.Text = CurrentBook.NumOfPages.ToString();
            releasetime.Text = CurrentBook.ReleaseTime.ToString();
            price.Text = CurrentBook.MainPrice.ToString();
            image.Text = CurrentBook.ImageAddress;
            type.Text = CurrentBook.booktype.ToString();
            category.Text = CurrentBook.category.ToString();
            author.Text = CurrentBook.author.FullName;
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            Book newBook = new Book(title.Text, desc.Text, int.Parse(page.Text), int.Parse(releasetime.Text), 0, double.Parse(price.Text), image.Text, new Author(author.Text), (BookType)Enum.Parse(typeof(BookType), type.Text), (Category)Enum.Parse(typeof(Category), category.Text), double.Parse(price.Text));

            bookDataAccess.EditBook(CurrentBook.ID, newBook);
            this.Close();
        }
    }
}
