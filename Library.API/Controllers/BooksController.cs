using Library.Business.Services;
using Library.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }
        private readonly IBookService _bookService;

        [HttpGet]
        public IActionResult GetAll()
        {
            var books = _bookService.GetAll();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = _bookService.GetById(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public IActionResult Add(Book book)
        {
            _bookService.Add(book);
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Book book)
        {
            var updatedBook = _bookService.Update(id, book);

            if (updatedBook == null)
            {
                return NotFound();
            }

            return Ok(updatedBook);
        }

    }
}
