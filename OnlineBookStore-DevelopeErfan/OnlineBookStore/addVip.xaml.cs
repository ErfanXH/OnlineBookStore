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
    public partial class addVip : Window
    {
        vipDataAccess vipdataaccess;
        public addVip(vipDataAccess vipdataaccess)
        {
            InitializeComponent();
            this.vipdataaccess = vipdataaccess;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(TxbTime.Text, "^[1-9][0-9]$"))
            {
                MessageBox.Show("The Time isn't correct!");
                return;
            }
            int time = int.Parse(TxbTime.Text.ToString());
            double price;
            try
            {
                price = double.Parse(TxbPrice.Text.ToString());
            }
            catch
            {
                MessageBox.Show("The Price isn't correct!");
                return;
            }
            if (price <= 0)
            {
                MessageBox.Show("The Price isn't correct!");
                return;
            }
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
