using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class vip
    {
        public int ID;
        public int Duration { get; set; }
        public double Price { get; set; }

        public int getID { get { return ID; } }

        public static ObservableCollection<vip> vips = new ObservableCollection<vip>(); //for ID

        public vip(int time, double cost)
        {
            Duration = time;
            Price = cost;
            vips.Add(this);
            ID_Generator();
        }

        public void ID_Generator()
        {
            this.ID = vips.Max(x => x.ID) + 1;
        }
    }
}
