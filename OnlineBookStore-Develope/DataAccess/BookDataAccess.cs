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
using Microsoft.Data.SqlClient;

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
            sqlCommand.ExecuteNonQuery();


            string command2 = "Select * from T_BookSales";
            SqlDataAdapter adapter2 = new SqlDataAdapter(command2, connection);
            DataTable dataTable2 = new DataTable();
            adapter2.Fill(dataTable2);
            SqlCommand sqlCommand2 = new SqlCommand(command2, connection);
            sqlCommand2.ExecuteNonQuery();


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
                int year = int.Parse(dataTable.Rows[i][9].ToString());
                string pdf = dataTable.Rows[i][14].ToString();

                double TotalSale = 0;
                if ( i < dataTable2.Rows.Count)
                {
                    TotalSale = double.Parse(dataTable2.Rows[i][1].ToString());
                }

                double CurrentPrice;
                if (dataTable.Rows[i][11].ToString() != "")
                    CurrentPrice = double.Parse(dataTable.Rows[i][11].ToString());
                else
                    CurrentPrice = price;

                int DiscountStart = 0, DiscountDuration = 0, n;
                if (int.TryParse(dataTable.Rows[i][12].ToString(), out n) && int.TryParse(dataTable.Rows[i][13].ToString(), out n))
                {
                    DiscountStart = int.Parse(dataTable.Rows[i][12].ToString());
                    DiscountDuration = int.Parse(dataTable.Rows[i][13].ToString());
                }

                if (DiscountStart + DiscountDuration == DateTime.Now.Day)
                {
                    CurrentPrice = price;
                    DiscountDuration = 0;
                    DiscountStart = 0;
                }

                Book book = new Book(title, desc, page, year, rate, price, address, author, type, cat, CurrentPrice, pdf, TotalSale);
                book.ID = id;
                book.DiscountDuration = DiscountDuration;
                book.DiscountStartTime = DiscountStart;

                bool CanAdd = true;
                for (int j = 0; j < dataTable2.Rows.Count; j++) 
                {
                    if (int.Parse(dataTable2.Rows[j][0].ToString()) == book.ID)
                    {
                        CanAdd = false;
                    }
                }

                if (CanAdd)
                {
                    int zero = 0;
                    string command10 = "Insert into T_BookSales values( '" + book.ID + "', '" + zero + "' )";
                    SqlCommand sqlCommand10 = new SqlCommand(command10, connection);
                    sqlCommand10.ExecuteNonQuery();
                }

                Books.Add(book);
            }

            //Read Rating
            string command1 = "Select * from T_BooksRating";
            SqlDataAdapter adapter1 = new SqlDataAdapter(command1, connection);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            SqlCommand sqlCommand1 = new SqlCommand(command1, connection);
            sqlCommand1.ExecuteNonQuery();

            for (int n = 0; n < Books.Count; n++)
            {
                List<int> Rates = new List<int>();
                for (int i = 0; i < dataTable1.Rows.Count; i++)
                {
                    int id = int.Parse(dataTable1.Rows[i][0].ToString());
                    string email = dataTable1.Rows[i][1].ToString();
                    int rate = int.Parse(dataTable1.Rows[i][2].ToString());

                    if (Books[n].ID == id)
                    {
                        Rates.Add(rate);
                    }
                }
                if (Rates.Count != 0)
                    Books[n].Rating = Rates.Average();
                else 
                    Books[n].Rating = 0;    
            }


            connection.Close();
        }
        public void AddBook(Book book)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lenovo\source\repos\VisionAcademy\DataAccess\BooksDatabase.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();
            string command; int zero = 0;
            command = "Insert into BooksTable values('" + book.ID + "' , '" + book.Title + "' , '" + book.Description + "' , '" + book.Rating + "','" + book.MainPrice + "' , '" + book.ImageAddress + "' , '" + book.author.FullName + "' , '" + book.booktype.ToString() + "' , '" + book.NumOfPages + "' , '" + DateTime.Now + "' , '" + book.category.ToString() + "' , '" + book.MainPrice + "' , '" + zero + "' , '" + zero + "', '"+book.PDFAddress+"' )";       //CurrentPrice !!!!
            SqlCommand sqlCommand = new SqlCommand(command, connection);
            sqlCommand.ExecuteNonQuery();

            string command2 = "Insert into T_BookSales values( '"+book.ID+"', '"+zero+"' )";
            SqlCommand sqlCommand2 = new SqlCommand(command2, connection);
            sqlCommand2.ExecuteNonQuery();

            connection.Close();

            Books.Add(book);
        }

        public void AddRatingBook(Book book, User user, int newRate)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lenovo\source\repos\VisionAcademy\DataAccess\BooksDatabase.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();
            string command1 = "Select * from T_BooksRating";
            SqlDataAdapter adapter1 = new SqlDataAdapter(command1, connection);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            SqlCommand sqlCommand1 = new SqlCommand(command1, connection);
            sqlCommand1.ExecuteNonQuery();

            //Check If User Has Already Added a Vote
            bool HasRated = false;
            for (int i = 0; i < dataTable1.Rows.Count; i++)
            {
                int id = int.Parse(dataTable1.Rows[i][0].ToString());
                string email = dataTable1.Rows[i][1].ToString();
                if (book.ID == id && user.Email == email)
                {
                    HasRated = true;
                }
            }

            if (!HasRated)
            {
                //Apply Changes
                for (int n = 0; n < Books.Count; n++)
                {
                    List<int> Rates = new List<int>();
                    for (int i = 0; i < dataTable1.Rows.Count; i++)
                    {
                        int id = int.Parse(dataTable1.Rows[i][0].ToString());
                        string email = dataTable1.Rows[i][1].ToString();
                        int rate = int.Parse(dataTable1.Rows[i][2].ToString());

                        if (Books[n].ID == id)
                        {
                            Rates.Add(rate);
                        }
                    }
                    if (Books[n].ID == book.ID)
                    {
                        // Add to DataBase
                        string command = "Insert into T_BooksRating values('" + book.ID + "' , '"+ user.Email +"' , '"+ newRate +"')";     
                        SqlCommand sqlCommand = new SqlCommand(command, connection);
                        sqlCommand.ExecuteNonQuery();
                         
                        Rates.Add(newRate);
                    }
                    if (Rates.Count != 0)
                        Books[n].Rating = Rates.Average();
                    else
                        Books[n].Rating = 0;
                }
            }

            connection.Close();
        }

        public void EditBook(int id, Book newBook) 
        {
            Book oldBook = Books.Where(x => x.ID == id).First();
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lenovo\source\repos\VisionAcademy\DataAccess\BooksDatabase.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();

            string command = "update BooksTable set Title = '" + newBook.Title + "', Description = '" + newBook.Description + "', Rating = '" + newBook.Rating + "', MainPrice = '" + newBook.MainPrice + "', ImageAddress = '" + newBook.ImageAddress + "', AuthorName = '" + newBook.author.FullName + "' , BookTypeStr = '" + newBook.booktype.ToString() + "', Page = '" + newBook.NumOfPages + "' , Time = '" + newBook.ReleaseTime + "' , CategoryStr = '" + newBook.category.ToString() + "', CurrentPrice = '" + newBook.Price + "' , DiscountStartTime = '" + newBook.DiscountStartTime + "' , DiscountDuration = '" + newBook.DiscountDuration + "', pdfAddress '" + newBook.PDFAddress + "' where Id = '" + id + "'";
            SqlCommand sqlCommand = new SqlCommand(command, connection);
            sqlCommand.ExecuteNonQuery();

            int index = Books.IndexOf(oldBook);
            Books[index] = newBook;


            connection.Close();
        }

        public void UpdateTotalSale(Book book, double value)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lenovo\source\repos\VisionAcademy\DataAccess\BooksDatabase.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();

            double newTotal = book.AmountOfSales + value;
            string command = "update BooksTable set ID = '" + book.ID + "', SaleAmount = '" + newTotal + "' where Id = '" + book.ID + "'";
            SqlCommand sqlCommand = new SqlCommand(command, connection);
            sqlCommand.ExecuteNonQuery();

            int index = Books.IndexOf(book);
            Books[index].AmountOfSales = book.AmountOfSales;

            connection.Close();
        }

        public void SetDiscount(double percentage, Book book, int time)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lenovo\source\repos\VisionAcademy\DataAccess\BooksDatabase.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();

            double newCurrent = ((100 - percentage) / 100) * book.MainPrice;

            string command = "update BooksTable set Title = '" + book.Title + "', Description = '" + book.Description + "', Rating = '" + book.Rating + "', MainPrice = '" + book.MainPrice + "', ImageAddress = '" + book.ImageAddress + "', BookTypeStr = '" + book.booktype.ToString() + "', CategoryStr = '" + book.category.ToString() + "', CurrentPrice = '" + newCurrent + "', DiscountStartTime = '" + DateTime.Now.Day + "', DiscountDuration = '" + time + "' , pdfAddress '" + book.PDFAddress + "' where Id = '" + book.ID + "'";
            SqlCommand sqlCommand = new SqlCommand(command, connection);
            sqlCommand.ExecuteNonQuery();

            int index = Books.IndexOf(book);
            Books[index].Price = ((100 - percentage) * Books[index].MainPrice) / 100;

            connection.Close();
        }

        public void ChangeBookType(Book book, BookType bookType)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lenovo\source\repos\VisionAcademy\DataAccess\BooksDatabase.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();

            string command = "update BooksTable set Title = '" + book.Title + "', Description = '" + book.Description + "', Rating = '" + book.Rating + "', Price = '" + book.MainPrice + "', ImageAddress = '" + book.ImageAddress + "', BookTypeStr = '" + bookType.ToString() + "', CategoryStr = '" + book.category.ToString() + "', CurrentPrice = '" + book.Price + "' , pdfAddress = '" + book.PDFAddress +"' where Id = '" + book.ID + "'";
            SqlCommand sqlCommand = new SqlCommand(command, connection);
            sqlCommand.ExecuteNonQuery();

            int index = 0;
            for (int i = 0; i < Books.Count; i++)
                if (Books[i].ID == book.ID)
                    index = i;

            Books[index].booktype = bookType;
            int id = Books[index].ID;
            Books[index] = new Book(book.Title, book.Description, book.NumOfPages, book.ReleaseTime, book.Rating, book.MainPrice, book.ImageAddress, book.author, bookType, book.category, book.Price, book.PDFAddress, book.AmountOfSales) { ID = id };

            connection.Close();
        }

        public void RemoveBook(Book book)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lenovo\source\repos\VisionAcademy\DataAccess\BooksDatabase.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();

            string command = "delete from BooksTable where Id = '" + book.ID + "' ";
            SqlCommand sqlCommand = new SqlCommand(command, connection);
            sqlCommand.ExecuteNonQuery();

            int index = Books.IndexOf(book);
            Books.RemoveAt(index);

            connection.Close();
        }
    }
}
