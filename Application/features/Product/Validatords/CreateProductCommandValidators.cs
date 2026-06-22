using FluentValidation;
using PrimeShop.Application.features.Product.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Product.Validatords
{
    public class CreateProductCommandValidators : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidators()
        {
            RuleFor(c => c.Price)
                    .GreaterThan(0).WithMessage("Price must be greater than 0.");
            RuleFor(c => c.StockQuantity)
              .GreaterThanOrEqualTo(0).WithMessage("Stock quantity cannot be negative.");

            RuleFor(c => c.Name).NotEmpty().WithMessage("this field requird");
            RuleFor(c => c.CategoryId).NotEmpty().WithMessage("this field requird");
            RuleFor(c => c.Description).NotEmpty().WithMessage("this field requird");

      

        }

    }
}



