using AutoMapper;
using Library.Business.Dtos;
using Library.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Business.Mapping
{
    public class LoanProfile : Profile
    {
        public LoanProfile()
        {
            CreateMap<Loan, LoanDto>();
        }
    }
}
