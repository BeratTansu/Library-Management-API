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

        public void Add(Book book)
        {
            _bookRepository.Add(book);
        }

        public List<Book> GetAll()
        {
            return _bookRepository.GetAll();
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

        public Book? Update(int id, Book book)
        {
            var existingBook = _bookRepository.GetById(id);

            if (existingBook == null)
            {
                return null;
            }

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.PublicationDate = book.PublicationDate;

            _bookRepository.Update(existingBook);
            return existingBook;
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
