using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using DataAccess.Models;
using System.Collections.ObjectModel;

namespace DataAccess
{
    public class AuthorDataAccess
    {
        public ObservableCollection<Author> Authors = new ObservableCollection<Author>();

        public AuthorDataAccess()
        {
            ReadData();
        }
        public Author FindAuthor(string FullName)
        {
            return Authors.Where(x => x.FullName == FullName).First();
        }
        private void ReadData() //Reads All Data Of Authors
        {
            
        }
        public void AddAuthor()
        {

        }
        public void EditAuthor()
        {

        }
        public void RemoveAuthor()
        {

        }
    }
}
