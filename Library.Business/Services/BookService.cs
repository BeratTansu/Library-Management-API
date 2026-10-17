using Library.DataAccess.Repositories;
using Library.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Business.Services
{
    public class BookService : IBookService
    {
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        private readonly IBookRepository _bookRepository;

        public void Add(Book book)
        {
            _bookRepository.Add(book);
        }

        public List<Book> GetAll()
        {
            return _bookRepository.GetAll();
        }

        public Book? GetById(int id)
        {
            return _bookRepository.GetById(id);
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
    }
}
