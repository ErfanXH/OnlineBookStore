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
    /// <summary>
    /// Interaction logic for BookMark.xaml
    /// </summary>
    public partial class BookMark : Window
    {
        User user;
        Book CurrentBook;
        public BookMark(User user)
        {
            InitializeComponent();
            MarkedGrid.ItemsSource = user.BookMark;
            BoughtGrid.ItemsSource = user.Books;
            this.user = user;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnBought_Click(object sender, RoutedEventArgs e)
        {
            HomePanel.Visibility = Visibility.Collapsed;
            MarkedPanel.Visibility = Visibility.Collapsed;
            BoughtPanel.Visibility = Visibility.Visible;
        }

        private void btnMarked_Click(object sender, RoutedEventArgs e)
        {
            HomePanel.Visibility = Visibility.Collapsed;
            MarkedPanel.Visibility = Visibility.Visible;
            BoughtPanel.Visibility = Visibility.Collapsed;
        }

        private void MarkedGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MarkedGrid.SelectedIndex >= 0)
            {
                CurrentBook = MarkedGrid.SelectedItem as Book;
                lbDescription.Content = CurrentBook.Description;
            }
        }

        private void BoughtGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BoughtGrid.SelectedIndex >= 0)
            {
                CurrentBook = BoughtGrid.SelectedItem as Book;
                //show pdf of book
            }
        }
    }
}