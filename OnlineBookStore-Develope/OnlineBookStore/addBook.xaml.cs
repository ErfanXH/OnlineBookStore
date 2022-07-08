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
            string Title = title.Text.Trim();
            if (Title == "")
            {
                MessageBox.Show("The Title isn't correct!");
                return;
            }
            string Desc = desc.Text.Trim();
            if (Desc == "")
            {
                MessageBox.Show("The Description isn't correct!");
                return;
            }
            if (!Regex.IsMatch(page.Text, "^[1-9][0-9]$"))
            {
                MessageBox.Show("The Number of Page isn't correct!");
                return;
            }
            int Page = int.Parse(page.Text);
            if (!Regex.IsMatch(releasetime.Text, "^[1-9][0-9]$"))
            {
                MessageBox.Show("The Release Time isn't correct!");
                return;
            }
            int ReleaseTime = int.Parse(releasetime.Text);
            double Price;
            try
            {
                Price = double.Parse(price.Text);
            }
            catch
            {
                MessageBox.Show("The Price isn't correct!");
                return;
            }
            if (Price <= 0)
            {
                MessageBox.Show("The Price isn't correct!");
                return;
            }
            if (author.Text.Trim() == "")
            {
                MessageBox.Show("The Author's name isn't correct!");
                return;
            }
            BookType bookType;
            try
            {
                bookType = (BookType)Enum.Parse(typeof(BookType), type.Text);
            }
            catch
            {
                MessageBox.Show("The Type of Book isn't correct!");
                return;
            }
            Category cate;
            try
            {
                cate = (Category)Enum.Parse(typeof(Category), category.Text);
            }
            catch
            {
                MessageBox.Show("The Category isn't correct!");
                return;
            }

            Book newBook = new Book(Title, Desc, Page, ReleaseTime, 0, Price, image.Text, new Author(author.Text), bookType, cate, Price, pdf.Text, 0);

            bookDataAccess.AddBook(newBook);
            this.Close();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}