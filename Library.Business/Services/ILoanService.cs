using Library.Business.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Business.Services
{
    public interface ILoanService
    {
        BorrowBookResult Borrow(int bookId, int userId);
        ReturnBookResult Return(int loanId, int userId);
    }
}
