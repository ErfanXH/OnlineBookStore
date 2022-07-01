using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public interface IBook
    {
        string Name { get; set; }
        string Author { get; set; }
        double Price { get; set; }
        string Genre { get; set; }
        int Rate { get; set; }
        int NumberOfSales { get; set; }
        double RevenueOfSales { get; set; }
        public int Released { get; set; }
        string Information();
        string Discription();
    }
}
