using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Author
    {
        public int ID = 0;  //delete ??
        public string FullName { get; set; }

        public ObservableCollection<Book> Books = new ObservableCollection<Book>();

        static ObservableCollection<Author> Authors = new ObservableCollection<Author>();

        public Author(string Name)
        {
            FullName = Name;
            Authors.Add(this);
            ID_Generator();
        }
        private void ID_Generator()
        {
            this.ID = Authors.Max(x => x.ID) + 1;
        }
        public string BIO()
        {
            return FullName + "\n" + Books.Select(x => x.Title + " ");
        }
    }
}
