using Library.DataAccess.Context;
using Library.Entities;
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

        public bool HasActiveLoan(int bookId)
        {
            return _context.Loans.Any(l => l.BookId == bookId && l.ReturnDate == null);
        }

        public void Add(Loan loan)
        {
            _context.Loans.Add(loan);
            _context.SaveChanges();
        }

        public Loan? GetById(int id)
        {
            return _context.Loans.FirstOrDefault(l => l.Id == id);
        }

        public void Update(Loan loan)
        {
            _context.Loans.Update(loan);
            _context.SaveChanges();
        }
    }
}
