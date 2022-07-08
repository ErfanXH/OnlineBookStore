using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class User : IUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public float AccountBalance { get; set; } = 0;
        public bool VIP { get; set; } = false;
        public ObservableCollection<Book> BasketOfGoods { get; set; } = new ObservableCollection<Book>();
        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>();
        public ObservableCollection<Book> BookMark { get; set; } = new ObservableCollection<Book>();
    }
}
