using FluentValidation;
using PrimeShop.Application.features.Product.Dtos;

namespace PrimeShop.Application.features.Product.Validatords
{
    public class DeleteProductCommandValidators : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidators()
        {
            RuleFor(c => c.Id)
                    .NotEmpty().WithMessage("Product Id is required.")
                    .NotEqual(Guid.Empty).WithMessage("Invalid Product ID.");

          



        }

    }
}
