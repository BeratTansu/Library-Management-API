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
        public BookService(IBookRepository bookRepository, ILoanRepository loanRepository)
        {
            _bookRepository = bookRepository;
            _loanRepository = loanRepository;
        }

        private readonly IBookRepository _bookRepository;
        private readonly ILoanRepository _loanRepository;

        public BookDto Add(CreateBookDto createBookDto)
        {
            var book = new Book
            {
                Title = createBookDto.Title,
                Author = createBookDto.Author,
                PublicationDate = createBookDto.PublicationDate,
            };

            _bookRepository.Add(book);

            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                PublicationDate = book.PublicationDate,
                Status = "Available"
            };
        }

        public List<BookDto> GetAll()
        {
            var books = _bookRepository.GetAll();
            var bookDtos = new List<BookDto>();

            foreach (var book in books)
            {
                bookDtos.Add(new BookDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    PublicationDate = book.PublicationDate,
                    Status = _loanRepository.HasActiveLoan(book.Id) ? "OnLoan" : "Available"
                });
            }

            return bookDtos;
        }

        public BookDto? GetById(int id)
        {
            var book = _bookRepository.GetById(id);

            if (book == null)
                return null;

            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                PublicationDate = book.PublicationDate,
                Status = _loanRepository.HasActiveLoan(book.Id) ? "OnLoan" : "Available"
            };
        }

        public BookDto? Update(int id, UpdateBookDto updateBookDto)
        {
            var existingBook = _bookRepository.GetById(id);

            if (existingBook == null)
            {
                return null;
            }

            existingBook.Title = updateBookDto.Title;
            existingBook.Author = updateBookDto.Author;
            existingBook.PublicationDate = updateBookDto.PublicationDate;

            _bookRepository.Update(existingBook);
            return new BookDto
            {
                Id = existingBook.Id,
                Title = existingBook.Title,
                Author = existingBook.Author,
                PublicationDate = existingBook.PublicationDate,
                Status = _loanRepository.HasActiveLoan(existingBook.Id) ? "OnLoan" : "Available"
            };
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
