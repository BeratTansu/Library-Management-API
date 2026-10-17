using Library.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DataAccess.Repositories
{
    public interface IBookRepository
    {
        List<Book> GetAll();

        Book? GetById(int id);

        void Add(Book book);

        Book Update(Book book);

        void Delete(Book book);
    }
}
