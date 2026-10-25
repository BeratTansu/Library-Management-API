using AutoMapper;
using Library.Business.Dtos;
using Library.Business.Results;
using Library.DataAccess.Repositories;
using Library.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Business.Services
{
    public class LoanService : ILoanService
    {
        public LoanService(IBookRepository bookRepository, ILoanRepository loanRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _loanRepository = loanRepository;
            _mapper = mapper;
        }

        private readonly IBookRepository _bookRepository;
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;

        public BorrowBookResult Borrow(int bookId, int userId)
        {
            var book = _bookRepository.GetById(bookId);
            if (book == null)
            {
                return new BorrowBookResult { Status = BorrowBookStatus.BookNotFound};
            }

            if (_loanRepository.HasActiveLoan(bookId)){
                return new BorrowBookResult { Status = BorrowBookStatus.BookOnLoan };
            }

            var loan = new Loan
            {
                BookId = bookId,
                UserId = userId,
                LoanDate = DateTime.UtcNow
            };

            _loanRepository.Add(loan);

            return new BorrowBookResult
            {
                Status = BorrowBookStatus.Success,
                Loan = _mapper.Map<LoanDto>(loan)
            };
        }

        public ReturnBookResult Return(int loanId, int userId)
        {
            var loan = _loanRepository.GetById(loanId);
            if (loan == null)
            {
                return new ReturnBookResult { Status = ReturnBookStatus.LoanNotFound };
            }

            if (loan.UserId != userId)
            {
                return new ReturnBookResult { Status = ReturnBookStatus.NotOwner};
            }

            if (loan.ReturnDate != null)
            {
                return new ReturnBookResult { Status = ReturnBookStatus.AlreadyReturned };
            }

            loan.ReturnDate = DateTime.UtcNow;
            _loanRepository.Update(loan);

            return new ReturnBookResult
            {
                Status = ReturnBookStatus.Success,
                Loan = _mapper.Map<LoanDto>(loan)
            };
        }
    }
}
