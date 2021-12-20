using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookClassLib;
using BookClassLib.Models;
using Microsoft.Extensions.Logging;

namespace BookWebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookAPIController : ControllerBase
    {
        private readonly Ibooks ibooks;

        private readonly ILogger<BookAPIController> logger;

        public BookAPIController(Ibooks ibooks, ILogger<BookAPIController> logger = null)
        {
            this.ibooks = ibooks;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Book> books = ibooks.Get();
            if (books != null)
            {
                logger.LogInformation("API call to get all books");
                return Ok(books);
            }

            return NotFound();
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Book book = ibooks.GetByID(id);
            if(book!=null)
            {
                return Ok(book);
            }
            return NotFound();

        }

        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid");
            }
            if (book == null)
            {
                ModelState.AddModelError("Object", "Object is null");
                return BadRequest(ModelState);
            }
            var createdbook=  ibooks.post(book);
            return CreatedAtAction(nameof(Get), new { id = createdbook.BookNo}, createdbook);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Book book = ibooks.GetByID(id);
            if (book == null)
            {
                return NotFound();
            }
            Int16 x = (Int16)new object();

            ibooks.Delete(id);
            return NoContent();

        }

        [HttpPut]
        public IActionResult Edit([FromBody] Book book)
        {
            if (book == null)
            {
                ModelState.AddModelError("Object", "Object is null");
                return BadRequest(ModelState);
            }
            ibooks.put(book);
            logger.LogInformation("Calling API to edit books");
            return NoContent();

        }

    }
}
