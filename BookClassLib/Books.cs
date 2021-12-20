using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookClassLib.Models;
using Microsoft.Extensions.Logging;

namespace BookClassLib
{
    public class Books:BookDatabaseContext, Ibooks
    {
        private readonly ILogger<Books> logger;
        private readonly BookDatabaseContext bookDatabaseContext;
        public Books(ILogger<Books> Logger, BookDatabaseContext bookDatabaseContext)
        {
            logger = Logger;
            this.bookDatabaseContext = bookDatabaseContext;
        }


        public List<Book> Get()
        {
            logger.LogInformation("Getting all books");
            return bookDatabaseContext.Book.ToList();
        }

        public Book GetByID(int id)
        {
            logger.LogInformation("Getting book by id test");
            return bookDatabaseContext.Book.Find(id);
        }

        public  Book post(Book book)
        {
            logger.LogInformation("adding book");
            var result=   bookDatabaseContext.Book.Add(book);
            bookDatabaseContext.SaveChangesAsync();
            return result.Entity;
        }

        public void put(Book book)
        {
            logger.LogInformation("Editing book");
            Book books = bookDatabaseContext.Book.Find(book.BookNo);
            books.Author = book.Author;
            books.Name = book.Name;
            books.Publisher = book.Publisher;
            bookDatabaseContext.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            logger.LogInformation("Deleting book by id");
            Book books = bookDatabaseContext.Book.Find(id);
            bookDatabaseContext.Book.Remove(books);
            bookDatabaseContext.SaveChangesAsync();
        }

    }
}
