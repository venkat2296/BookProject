using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using BookClassLib.Models;
using BookMVC.Models;
using Microsoft.Extensions.Logging;
using NLog;

namespace BookMVC.Controllers
{
    public class BookController : Controller
    {
        private readonly ILogger<BookController> logger;
        private readonly Logger _logger = LogManager.GetLogger("allfiles");      
        public BookController(ILogger<BookController> logger)
        {
            this.logger = logger;
            
            // this._bookDBContext = _bookDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> Allbooks()
        {
           // _logger.Error("Checking for error");
            _logger.Info("In MVC method to get all books");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:22978/");
            List<BookViewModel> books  = await client.GetFromJsonAsync<List<BookViewModel>>("api/BookAPI");
            return View(books);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            _logger.Info("In MVC method to get book by ID");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:22978/");
            BookViewModel book = await client.GetFromJsonAsync<BookViewModel>($"api/BookAPI/{id}");
            return View(book);
        }

        [HttpGet]
        public  IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookViewModel model)
        {
            _logger.Info("In MVC method to create books");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:22978/");
            await client.PostAsJsonAsync<BookViewModel>("api/BookAPI", model);

            return RedirectToAction("Allbooks");
        }

       
        public async Task<IActionResult> Delete(int id)
        {
            _logger.Info("In MVC method to delete books");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:22978/");
            var result= await client.DeleteAsync($"api/BookAPI/{id}");
            return RedirectToAction("Allbooks");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            _logger.Info("In MVC method to edit books");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:22978/");
            BookViewModel book = await client.GetFromJsonAsync<BookViewModel>($"api/BookAPI/{id}");
            return View(book);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(BookViewModel model)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:22978/");
            var result= await client.PutAsJsonAsync<BookViewModel>("api/BookAPI", model);
            return RedirectToAction("Allbooks");
        }
    }
}
