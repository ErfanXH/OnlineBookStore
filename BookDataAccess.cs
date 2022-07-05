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
    public class BookDataAccess
    {
        public ObservableCollection<Book> Books = new ObservableCollection<Book>();
        public BookDataAccess()
        {
            ReadData();
        }
        private void ReadData() //Reads All Data Of Books
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lenovo\source\repos\VisionAcademy\DataAccess\BooksDatabase.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();

            string command = "Select * from BooksTable";
            SqlDataAdapter adapter = new SqlDataAdapter(command, connection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            SqlCommand sqlCommand = new SqlCommand(command, connection);
            sqlCommand.BeginExecuteNonQuery();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                int id = int.Parse(dataTable.Rows[i][0].ToString());
                string title = dataTable.Rows[i][1].ToString();
                string desc = dataTable.Rows[i][2].ToString();
                double rate = double.Parse(dataTable.Rows[i][3].ToString());
                double price = double.Parse(dataTable.Rows[i][4].ToString());
                string address = dataTable.Rows[i][5].ToString();
                Author author = new Author(dataTable.Rows[i][6].ToString());
                BookType type = (BookType)Enum.Parse(typeof(BookType), dataTable.Rows[i][7].ToString());
                int page = int.Parse(dataTable.Rows[i][8].ToString());
                Category cat = (Category)Enum.Parse(typeof(Category), dataTable.Rows[i][10].ToString());

                Book book = new Book(title, desc, page, DateTime.Now, rate, price, address, author, type, cat);
                book.ID = id;
                Books.Add(book);
            }

            connection.Close();
        }
        public void AddBook(Book book)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lenovo\source\repos\VisionAcademy\DataAccess\BooksDatabase.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();
            string command;
            command = "Insert into BooksTable values('"+book.ID+ "','" + book.Title+ "','" + book.Description + "','" + book.Rating + "''" + book.Price + "', '" + book.ImageAddress + "','" + book.author.FullName + "', '" + book.booktype.ToString() + "')";
            SqlCommand sqlCommand = new SqlCommand(command, connection);
            sqlCommand.BeginExecuteNonQuery();
            connection.Close();

        }
        public void EditBook(int id, Book newBook)  //extra book would be added??!
        {
            Book oldBook = Books.Where(x => x.ID == id).First();
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lenovo\source\repos\VisionAcademy\DataAccess\BooksDatabase.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();

            string command = "update BooksTable set Title = '"+newBook.Title+"', Description = '"+newBook.Description+"', Rating = '"+newBook.Rating+"', Price = '"+newBook.Price+"', ImageAddress = '"+newBook.ImageAddress+"', BookTypeStr = '"+newBook.booktype.ToString()+"', CategoryStr = '"+newBook.category.ToString()+"' where Id = '"+id+"'";
            SqlCommand sqlCommand = new SqlCommand(command, connection);
            sqlCommand.BeginExecuteNonQuery();

            RemoveBook(newBook);    //delete extra

            connection.Close();
        }

        public void ChangeBookType(Book book, BookType bookType)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lenovo\source\repos\VisionAcademy\DataAccess\BooksDatabase.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();

            string command = "update BooksTable set Title = '" + book.Title + "', Description = '" + book.Description + "', Rating = '" + book.Rating + "', Price = '" + book.Price + "', ImageAddress = '" + book.ImageAddress + "', BookTypeStr = '" + bookType.ToString() + "', CategoryStr = '" + book.category.ToString() + "' where Id = '" + book.ID + "'";
            SqlCommand sqlCommand = new SqlCommand(command, connection);
            sqlCommand.ExecuteNonQuery();

            int index = 0;
            for (int i = 0; i < Books.Count; i++)
                if (Books[i].ID == book.ID)
                    index = i;

            Books[index].booktype = bookType;
            int id = Books[index].ID;
            Books[index] = new Book(book.Title, book.Description, book.NumOfPages, DateTime.Now, book.Rating, book.Price, book.ImageAddress, book.author, bookType, book.category) { ID = id };
           
            connection.Close();
        }

        public void RemoveBook(Book book)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lenovo\source\repos\VisionAcademy\DataAccess\BooksDatabase.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();

            string command = "delete from BooksTable where Id = '"+book.ID+"'";
            SqlCommand sqlCommand = new SqlCommand(command, connection);
            sqlCommand.BeginExecuteNonQuery();

            connection.Close();
        }
    }
}