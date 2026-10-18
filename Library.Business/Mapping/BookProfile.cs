using AutoMapper;
using Library.Business.Dtos;
using Library.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Business.Mapping
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.Status, opt => opt.Ignore());

            CreateMap<CreateBookDto, Book>();

            CreateMap<UpdateBookDto, Book>();
        }
    }
}
