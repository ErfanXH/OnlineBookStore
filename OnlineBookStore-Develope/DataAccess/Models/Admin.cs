using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Admin : IAdmin
    {
        public string Username { get; set; }
        public static string Password { get; set; } = "1234";
        public static double Balance { get; set; } = 0;
    }
}
