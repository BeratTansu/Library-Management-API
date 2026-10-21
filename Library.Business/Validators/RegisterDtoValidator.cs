using FluentValidation;
using Library.Business.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Business.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(250).WithMessage("First name cannot exceed 250 characters")
                .MinimumLength(2).WithMessage("First name must be at least 2 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(250).WithMessage("Last name cannot exceed 250 characters")
                .MinimumLength(2).WithMessage("Last name must be at least 2 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .MaximumLength(250).WithMessage("Email cannot exceed 250 characters")
                .EmailAddress().WithMessage("Email must be a valid email address.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MaximumLength(72).WithMessage("Password cannot exceed 72 characters")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.");
        }
    }
}
