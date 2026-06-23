using FluentValidation;
using PrimeShop.Application.features.Product.Dtos;

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

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(3, 100).WithMessage("Name must be between 3 and 100 characters.");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(c => c.CategoryId)
                .NotEmpty().WithMessage("Category ID is required.")
                .NotEqual(Guid.Empty).WithMessage("The provided Category ID is invalid.");

        }

    }
}



