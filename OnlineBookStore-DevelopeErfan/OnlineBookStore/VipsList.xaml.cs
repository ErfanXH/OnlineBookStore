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
    public partial class VipsList : Window
    {
        vipDataAccess vipdataaccess;
        public VipsList(vipDataAccess vipdataaccess)
        {
            InitializeComponent();
            this.vipdataaccess = vipdataaccess;
            ListBoxV.ItemsSource = vipdataaccess.Vips;
        }

        private void ListBoxV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Addvip_Click(object sender, RoutedEventArgs e)
        {
            addVip addvip = new addVip(vipdataaccess);
            addvip.ShowDialog();
        }

        private void Delvip_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxV.SelectedIndex >= 0)
            {
                vip selectedvip = ListBoxV.SelectedItem as vip;
                vipdataaccess.DeleteData(selectedvip);
            }
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
