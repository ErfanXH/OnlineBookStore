using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public interface IAdmin
    {
        string Username { get; set; }
        static string Password { get; set; }
    }
}
