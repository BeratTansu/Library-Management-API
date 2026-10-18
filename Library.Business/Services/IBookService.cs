using Library.Business.Dtos;
using Library.Business.Results;
using Library.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Business.Services
{
    public interface IBookService
    {
        List<Book> GetAll();

        BookDto? GetById(int id);

        void Add(Book book);

        Book? Update(int id, Book book);

        DeleteBookResult Delete(int id);
    }
}
