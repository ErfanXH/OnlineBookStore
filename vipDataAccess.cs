using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Models;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class vipDataAccess
    {
        public ObservableCollection<vip> Vips = new ObservableCollection<vip>();

        public vipDataAccess()
        {
            ReadData();
        }

        private void ReadData()
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lenovo\source\repos\HomePage\DataAccess\vip.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();

            string command = "Select * from VipTable";
            SqlDataAdapter adapter = new SqlDataAdapter(command, connection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            SqlCommand sqlCommand = new SqlCommand(command, connection);
            sqlCommand.BeginExecuteNonQuery();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                int ID = int.Parse(dataTable.Rows[i][0].ToString());
                int Duration = int.Parse(dataTable.Rows[i][1].ToString());
                double Price = double.Parse(dataTable.Rows[i][2].ToString());

                vip tmp = new vip(Duration, Price);
                tmp.ID = ID; 
                Vips.Add(tmp);
            }

            connection.Close();
        }

        public void AddData(vip v)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lenovo\source\repos\HomePage\DataAccess\vip.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();
            string command = "Insert into VipTable values('" + v.ID + "','" + v.Duration + "','" + v.Price + "')";
            SqlCommand sqlCommand = new SqlCommand(command, connection);
            sqlCommand.ExecuteNonQuery();

            Vips.Add(v);
            
            connection.Close();
        }

        public void DeleteData(vip v)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lenovo\source\repos\HomePage\DataAccess\vip.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();

            string command = "delete from VipTable where ID = '" + v.ID + "'";
            SqlCommand sqlCommand = new SqlCommand(command, connection);
            sqlCommand.ExecuteNonQuery();

            Vips.Remove(v);

            connection.Close();
        }
    }
}
