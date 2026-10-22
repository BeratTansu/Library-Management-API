using FluentValidation;
using Library.Business.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Business.Validators
{
    public class CreateLoanDtoValidator : AbstractValidator<CreateLoanDto>
    {
        public CreateLoanDtoValidator()
        {
            RuleFor(x => x.BookId)
                .GreaterThan(0).WithMessage("A valid id is required");
        }
    }
}
