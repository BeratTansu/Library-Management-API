using Library.Business.Dtos;
using Library.Business.Results;
using Library.Business.Services;
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
        public IActionResult Add(CreateBookDto createBookDto)
        {
            var bookDto = _bookService.Add(createBookDto);
            return CreatedAtAction(nameof(GetById), new { id = bookDto.Id }, bookDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateBookDto updateBookDto)
        {
            var updatedBookDto = _bookService.Update(id, updateBookDto);

            if (updatedBookDto == null)
            {
                return NotFound();
            }

            return Ok(updatedBookDto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _bookService.Delete(id);

            if (result == DeleteBookResult.NotFound)
            {
                return NotFound();
            }

            if (result == DeleteBookResult.HasLoans)
            {
                return Conflict("This book has loan history and cannot be deleted");
            }

            return NoContent();
        }

    }
}
