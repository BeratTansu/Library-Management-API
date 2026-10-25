using FluentValidation;
using Library.Business.Dtos;
using Library.Business.Results;
using Library.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Member")]
    public class LoansController : ControllerBase
    {
        public LoansController(ILoanService loanService, IValidator<CreateLoanDto> createLoanValidator)
        {
            _loanService = loanService;
            _createLoanValidator = createLoanValidator;
        }
        private readonly ILoanService _loanService;
        private readonly IValidator<CreateLoanDto> _createLoanValidator;

        [HttpPost]
        public IActionResult Borrow(CreateLoanDto createLoanDto)
        {
            var validationResult = _createLoanValidator.Validate(createLoanDto);

            if (!validationResult.IsValid)
            {
                return ValidationProblem(new ValidationProblemDetails(validationResult.ToDictionary()));
            }

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var result = _loanService.Borrow(createLoanDto.BookId, userId);

            if (result.Status == BorrowBookStatus.BookNotFound)
            {
                return NotFound();
            }

            if (result.Status == BorrowBookStatus.BookOnLoan)
            {
                return Conflict("This book is currently on loan");
            }

            return StatusCode(StatusCodes.Status201Created, result.Loan);
        }

        [HttpPost("{id}/return")]
        public IActionResult Return(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var result = _loanService.Return(id, userId);

            if (result.Status == ReturnBookStatus.LoanNotFound)
            {
                return NotFound();
            }

            if (result.Status == ReturnBookStatus.NotOwner)
            {
                return Forbid();
            }

            if (result.Status == ReturnBookStatus.AlreadyReturned)
            {
                return Conflict("This loan has already been returned");
            }

            return Ok(result.Loan);
        }
    }
}
