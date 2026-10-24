using FluentValidation;
using PrimeShop.Application.features.Contact.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Contact.Validators
{
    public class CreateContactMessageValidator
        : AbstractValidator<CreateContactMessageCommand>
    {
        public CreateContactMessageValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(256);

            RuleFor(x => x.Subject)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Message)
                .NotEmpty()
                .MaximumLength(5000);
        }
    }
}
