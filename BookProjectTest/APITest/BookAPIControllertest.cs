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
    public class BookAPIControllertest
    {
        private Ibooks _fakerepos;
        private readonly BookAPIController _controller;

        public BookAPIControllertest()
        {
            _fakerepos = new BookReposFake();
            _controller = new BookAPIController(_fakerepos, NullLogger<BookAPIController>.Instance);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            var okResult = _controller.Get();
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _controller.Get() as OkObjectResult;
            // Assert
            var items = Assert.IsType<List<Book>>(okResult.Value);
            Assert.Equal(3, items.Count);


        }

        [Fact]
        public void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            var notFoundResult = _controller.GetById(99999);
            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public void GetById_ExistingIDPassed_ReturnsOkResult()
        {
            // Arrange
            var Id = 1;
            // Act
            var okResult = _controller.GetById(Id);
            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsRightItem()
        {
            // Arrange
            var testGuid = 1;
            // Act
            var okResult = _controller.GetById(testGuid) as OkObjectResult; ;
            // Assert
            var items = Assert.IsType<Book>(okResult.Value);
            Assert.Equal(testGuid, (items.BookNo));
        }

        [Fact]
        public void AddMethodTestWithValidData()
        {
            Book book = new Book() { Author = "Sanjay", Name = "sathya", BookNo = 5, Publisher = "sona" };
            var CreatedResult = _controller.Post(book);
            Assert.IsType<CreatedAtActionResult>(CreatedResult);

        }

        [Fact]
        public void AddMethodTestWithInValidData()
        {
            Book book = new Book() { Name = "sathya", BookNo = 5, Publisher = "sona" };
            _controller.ModelState.AddModelError("Author", "Required");
            var BadRequestResult = _controller.Post(book);
            Assert.IsType<BadRequestObjectResult>(BadRequestResult);

        }

        [Fact]
        public void AddMethodTestWithValidDataHasItem()
        {
            Book book = new Book() { Author = "Sanjay", Name = "sathya", BookNo = 5, Publisher = "sona" };
            var CreatedResult = _controller.Post(book) as CreatedAtActionResult;
            var item = Assert.IsType<Book>(CreatedResult.Value);
            Assert.Equal(book.BookNo, item.BookNo);

        }


        [Fact]
        public void DeleteValidItem()
        {
            var NoContentobj = _controller.Delete(2) as NoContentResult;
            Assert.IsType<NoContentResult>(NoContentobj);

        }



        [Fact]
        public void DeleteInValidItem()
        {
            var NotFoundObj = _controller.Delete(200) as NotFoundResult;
            Assert.IsType<NotFoundResult>(NotFoundObj);

        }




        [Fact]
        public void DeleteValidItemWithCountCheck()
        {
            var NoContentobj = _controller.Delete(2) as NoContentResult;
            Assert.IsType<NoContentResult>(NoContentobj);
            var items = _controller.Get() as OkObjectResult;
            var itemcount = Assert.IsType<List<Book>>(items.Value);
            Assert.Equal(2, itemcount.Count);


        }

        [Fact]
        public void EditWithValidData()
        {
            Book book = new Book() { Author = "Sanjay", Name = "sathya", BookNo = 1, Publisher = "sona" };
            var Okobj = _controller.Edit(book);
            Assert.IsType<NoContentResult>(Okobj);



        }











    }
}
