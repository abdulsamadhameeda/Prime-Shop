using FluentValidation;
using PrimeShop.Application.features.Order.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Order.Validators
{
    public class GetMyOrdersQueryValidator : AbstractValidator<GetMyOrdersQuery>
    {
        public GetMyOrdersQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.Draw)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Start)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Length)
                .InclusiveBetween(1, 36);

            RuleFor(x => x.ToDate)
                .GreaterThanOrEqualTo(x => x.FromDate)
                .When(x => x.FromDate.HasValue && x.ToDate.HasValue);
        }
    }
}
