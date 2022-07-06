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
        public string Title { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public double Price { get; set; }
        public int NumOfPages { get; set; }
        public DateTime ReleaseTime { get; set; }
        public string ImageAddress { get; set; }
        public Category category { get; set; }
        public Author author { get; set; }
        public BookType booktype { get; set; }

        public static ObservableCollection<Book> books = new ObservableCollection<Book>();  //for ID

        public Book(string title, string description, int page, DateTime time, double rating, double price, string imageAddress, Author author, BookType booktype, Category category)
        {
            Title = title;
            Description = description;
            NumOfPages = page;
            ReleaseTime = time;
            Rating = rating;
            Price = price;
            ImageAddress = imageAddress;
            //  ImageAddress = $"Pivtures/{Title}.jpg";
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
