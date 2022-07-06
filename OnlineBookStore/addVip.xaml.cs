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
    public partial class addVip : Window
    {
        vipDataAccess vipdataaccess = new vipDataAccess();
        public addVip(vipDataAccess vipdataaccess)
        {
            InitializeComponent();
            this.vipdataaccess = vipdataaccess;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            int time = int.Parse(TxbTime.Text.ToString());
            double price = double.Parse(TxbPrice.Text.ToString());

            vip newV = new vip(time, price);

            vipdataaccess.AddData(newV);
            this.Close();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
