using System;
using Xunit;
using BookWebapi;
using BookClassLib;
using BookWebapi.Controllers;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using BookClassLib.Models;
namespace BookProjectTest
{
     public class BookServiceClassTest:BookTestBase
    {

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            var books = new Books(NullLogger<Books>.Instance, context);
            var okResult = books.Get();
            Assert.IsType<List<Book>>(okResult);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            
            var books = new Books(NullLogger<Books>.Instance, context);
            // Act
            var okResult = books.Get();
            // Assert
            var items = Assert.IsType<List<Book>>(okResult);
            Assert.Equal(3, items.Count);


        }

        [Fact]
        public void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            var books = new Books(NullLogger<Books>.Instance, context);
            var notFoundResult = books.GetByID(99999);

            // Assert
            Assert.Null(notFoundResult);
        }

        [Fact]
        public void GetById_ExistingIDPassed_ReturnsOkResult()
        {
            var books = new Books(NullLogger<Books>.Instance, context);
            // Arrange
            var Id = 1;
            // Act
            var okResult = books.GetByID(Id);
            // Assert
            Assert.IsType<Book>(okResult);
        }

        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsRightItem()
        {
            var books = new Books(NullLogger<Books>.Instance, context);
            // Arrange
            var testGuid = 1;
            // Act
            var okResult = books.GetByID(testGuid);
            // Assert
            var items = Assert.IsType<Book>(okResult);
            Assert.Equal(testGuid, (items.BookNo));
        }

        [Fact]
        public void AddMethodTestWithValidData()
        {
            var books = new Books(NullLogger<Books>.Instance, context);
            Book book = new Book() { Author = "Sanjay", Name = "sathya", BookNo = 5, Publisher = "sona" };
            var CreatedResult = books.post(book);            
            Assert.IsType<Book>(CreatedResult);

        }

        [Fact]
        public void AddMethodTestWithInValidData()
        { 
            var books = new Books(NullLogger<Books>.Instance, context);
            Book book = new Book() { Name = "sathya", BookNo = 5, Publisher = "sona" };
            var BadRequestResult = books.post(book);
            Assert.Null(BadRequestResult.Author);

        }

        [Fact]
        public void AddMethodTestWithValidDataHasItem()
        {
            var books = new Books(NullLogger<Books>.Instance, context);
            Book book = new Book() { Author = "Sanjay", Name = "sathya", BookNo = 5, Publisher = "sona" };
            var CreatedResult = books.post(book);
            var item = Assert.IsType<Book>(CreatedResult);            
            Assert.Equal(book.BookNo, item.BookNo);

        }


  



        [Fact]
        public void DeleteValidItemWithCountCheck()
        {
            var books = new Books(NullLogger<Books>.Instance, context);
            books.Delete(2);
            var items = books.Get();
            var itemcount = Assert.IsType<List<Book>>(items);
            Assert.Equal(2, itemcount.Count);


        }

        [Fact]
        public void EditWithValidData()
        {
            var books = new Books(NullLogger<Books>.Instance, context);
            Book book = new Book() { Author = "Sanjay", Name = "sathya", BookNo = 1, Publisher = "sona" };
            books.put(book);       
            Assert.Equal("Sanjay", book.Author);
            
        }
    }
}
