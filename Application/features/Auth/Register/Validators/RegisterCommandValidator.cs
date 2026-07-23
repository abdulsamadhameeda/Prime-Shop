using FluentValidation;
using PrimeShop.Application.features.Auth.Register.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Auth.Register.Validators
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(c => c.FirstName)
              .NotEmpty().WithMessage("First name is required.")
              .Length(2, 50).WithMessage("First name must be between 2 and 50 characters.");

            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .Length(2, 50).WithMessage("Last name must be between 2 and 50 characters.");
            RuleFor(c => c.Email)
                 .NotEmpty().WithMessage("Email address is required.")
                  .EmailAddress().WithMessage("A valid email address is required.");

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MinimumLength(8)
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one digit")
                .Matches(@"[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character");

            RuleFor(c => c.PhoneNumber)
                    .NotEmpty().WithMessage("Phone number is required.")
                    .Matches(@"^(?:\+962|00962|0)?7[789]\d{7}$")
                    .WithMessage("Invalid Jordanian phone number. Must start with 07X or +9627X followed by 7 digits.");
        }
    }
}
