using Library.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DataAccess.Repositories
{
    public interface ILoanRepository
    {
        bool HasLoansForBook(int bookId);

        bool HasActiveLoan(int bookId);
        void Add(Loan loan);
    }
}
