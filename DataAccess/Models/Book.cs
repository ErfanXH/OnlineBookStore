using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DataAccess.Models
{
    public enum BookType
    {
        Ordinary, VIP
    }
    public enum Category
    {
        Comedy, Drama, Horror,
        Sport, Social, Adventure,
        Kids, Teenage, Adult
    }
    public class Book
    {
        public int ID = 100;
        public int getID { get { return ID; } }//
        public string Title { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public double MainPrice { get; set; }//
        public double Price { get; set; }
        public int NumOfPages { get; set; }
        public int ReleaseTime { get; set; }
        public string ImageAddress { get; set; }
        public Category category { get; set; }
        public Author author { get; set; }
        public BookType booktype { get; set; }
        public int NumberOfSales { get; set; }//
        public int DiscountStartTime { get; set; }//
        public int DiscountDuration { get; set; }//

        public static ObservableCollection<Book> books = new ObservableCollection<Book>();

        public Book(string title, string description, int page, int time, double rating, double price, string imageAddress, Author author, BookType booktype, Category category, double Current)
        {
            Title = title;
            Description = description;
            NumOfPages = page;
            ReleaseTime = time;
            Rating = rating;
            MainPrice = price;
            Price = Current;
            ImageAddress = imageAddress;
            this.booktype = booktype;
            this.author = author;
            this.category = category;
            books.Add(this);
            ID_Generator();
        }
        private void ID_Generator()
        {
            this.ID = books.Max(x => x.ID) + 1;
        }

    }
}
