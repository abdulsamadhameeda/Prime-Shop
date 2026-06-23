

using FluentValidation;
using PrimeShop.Application.features.Product.Dtos;

namespace PrimeShop.Application.features.Product.Validatords
{
    public class UpdateProductCommandValidators : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidators()
        {
            RuleFor(c => c.Id)
                    .NotEmpty().WithMessage("Product Id is requiredd.")
                    .NotEqual(Guid.Empty).WithMessage("Invalid Product ID.");

            RuleFor(c => c.Price)
                    .GreaterThan(0).WithMessage("Price must be greater than 0.");
            RuleFor(c => c.StockQuantity)
                    .GreaterThanOrEqualTo(0).WithMessage("Stock quantity cannot be negative.");

            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is requird");
            RuleFor(c => c.CategoryId).NotEmpty().WithMessage("Category Id is requird");
            RuleFor(c => c.Description).NotEmpty().WithMessage("Description is requird");



        }

    }
}
