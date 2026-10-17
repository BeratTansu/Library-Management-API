using Library.DataAccess.Context;
using Library.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DataAccess.Repositories
{
    public class BookRepository : IBookRepository
    {
        public BookRepository(LibraryDbContext libraryDbContext)
        {
            _context = libraryDbContext;
        }

        private readonly LibraryDbContext _context;

        public void Add(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void Delete(Book book)
        {
            _context.Books.Remove(book);
            _context.SaveChanges();
        }

        public List<Book> GetAll()
        {
            return _context.Books.ToList();
        }

        public Book? GetById(int id)
        {
            return _context.Books.FirstOrDefault(b => b.Id == id);
        }

        public Book Update(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
            return book;
        }
    }
}
