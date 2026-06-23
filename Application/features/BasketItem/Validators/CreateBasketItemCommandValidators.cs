using FluentValidation;
using PrimeShop.Application.features.BasketItem.Dtos;

namespace PrimeShop.Application.features.BasketItem.Validatords
{
    public class CreateBasketItemCommandValidators : AbstractValidator<CreateBasketItemCommand>
    {
        public CreateBasketItemCommandValidators()
        {

            RuleFor(c => c.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

            RuleFor(c => c.ProductId).NotEmpty().WithMessage("Product Id is requird");

            RuleFor(c => c.BasketId).NotEmpty().WithMessage("Basket Id is requird");


        }
    }
}
