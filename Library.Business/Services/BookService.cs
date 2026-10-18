using AutoMapper;
using Library.Business.Dtos;
using Library.Business.Results;
using Library.DataAccess.Repositories;
using Library.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Business.Services
{
    public class BookService : IBookService
    {
        public BookService(IBookRepository bookRepository, ILoanRepository loanRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _loanRepository = loanRepository;
            _mapper = mapper;
        }

        private readonly IBookRepository _bookRepository;
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;

        private BookDto MapToBookDto(Book book)
        {
            var bookDto = _mapper.Map<BookDto>(book);
            bookDto.Status = _loanRepository.HasActiveLoan(book.Id) ? "OnLoan" : "Available";
            return bookDto;
        }

        public BookDto Add(CreateBookDto createBookDto)
        {
            var book = _mapper.Map<Book>(createBookDto);

            _bookRepository.Add(book);

            return MapToBookDto(book);
        }

        public List<BookDto> GetAll()
        {
            var books = _bookRepository.GetAll();
            var bookDtos = new List<BookDto>();

            foreach (var book in books)
            {
                bookDtos.Add(MapToBookDto(book));
            }

            return bookDtos;
        }

        public BookDto? GetById(int id)
        {
            var book = _bookRepository.GetById(id);

            if (book == null)
                return null;

            return MapToBookDto(book);
        }

        public BookDto? Update(int id, UpdateBookDto updateBookDto)
        {
            var existingBook = _bookRepository.GetById(id);

            if (existingBook == null)
            {
                return null;
            }

            _mapper.Map(updateBookDto, existingBook);

            _bookRepository.Update(existingBook);
            return MapToBookDto(existingBook);
        }

        public DeleteBookResult Delete(int id)
        {
            var book = _bookRepository.GetById(id);

            if (book == null)
            {
                return DeleteBookResult.NotFound;
            }

            if (_loanRepository.HasLoansForBook(id))
            {
                return DeleteBookResult.HasLoans;
            }

            _bookRepository.Delete(book);
            return DeleteBookResult.Deleted;
        }
    }
}
