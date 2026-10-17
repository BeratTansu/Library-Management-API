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
    }
}
