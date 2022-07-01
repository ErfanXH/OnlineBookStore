using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Book : IBook
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
        public string Genre { get; set; }
        public int Rate { get; set; }
        public int NumberOfSales { get; set; }
        public int Released { get; set; }
        public double RevenueOfSales { get; set; }
        public string Discription()
        {
            throw new NotImplementedException();
        }

        public string Information()
        {
            throw new NotImplementedException();
        }
    }
}
