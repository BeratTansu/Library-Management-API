using FluentValidation;
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
        public BooksController(IBookService bookService, IValidator<CreateBookDto> createBookValidator, IValidator<UpdateBookDto> updateBookValidator)
        {
            _bookService = bookService;
            _createBookValidator = createBookValidator;
            _updateBookValidator = updateBookValidator;
        }
        private readonly IBookService _bookService;
        private readonly IValidator<CreateBookDto> _createBookValidator;
        private readonly IValidator<UpdateBookDto> _updateBookValidator; 

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
            var validationResult = _createBookValidator.Validate(createBookDto);

            if (!validationResult.IsValid)
            {
                return ValidationProblem(new ValidationProblemDetails(validationResult.ToDictionary()));   
            }

            var bookDto = _bookService.Add(createBookDto);
            return CreatedAtAction(nameof(GetById), new { id = bookDto.Id }, bookDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateBookDto updateBookDto)
        {
            var validationResult = _updateBookValidator.Validate(updateBookDto);

            if (!validationResult.IsValid)
            {
                return ValidationProblem(new ValidationProblemDetails(validationResult.ToDictionary()));
            }

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
