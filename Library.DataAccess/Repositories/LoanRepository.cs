using Library.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DataAccess.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        public LoanRepository(LibraryDbContext dbContext)
        {
            _context = dbContext;
        }

        private readonly LibraryDbContext _context;

        public bool HasLoansForBook(int bookId)
        {
            return _context.Loans.Any(l => l.BookId == bookId);
        }
    }
}
