using Library.Business.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Business.Results
{
    public class ReturnBookResult
    {
        public ReturnBookStatus Status { get; set; }
        public LoanDto? Loan { get; set; }
    }
}
