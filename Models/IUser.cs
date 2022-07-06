using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public interface IUser
    {
        string FirtName { get; set; }
        string LastName { get; set; }
        string PhoneNumber { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        double AccountBalance { get; set; }
        bool VIP { get; set; }
        ObservableCollection<Book> BasketOfGoods { get; set; }
        ObservableCollection<Book> Books { get; set; }
        ObservableCollection<Book> BookMark { get; set; }
    }
}
