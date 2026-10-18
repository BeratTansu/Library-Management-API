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
        List<BookDto> GetAll();

        BookDto? GetById(int id);

        BookDto Add(CreateBookDto createBookDto);

        BookDto? Update(int id, UpdateBookDto updateBookDto);

        DeleteBookResult Delete(int id);
    }
}
