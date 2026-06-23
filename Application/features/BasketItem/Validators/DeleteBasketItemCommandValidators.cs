using FluentValidation;
using PrimeShop.Application.features.BasketItem.Dtos;


namespace PrimeShop.Application.features.BasketItem.Validatords
{
    public class DeleteBasketItemCommandValidators : AbstractValidator<DeleteBasketItemCommand>
    {
        public DeleteBasketItemCommandValidators()
        {
            RuleFor(c => c.Id)
                    .NotEmpty().WithMessage("Basket item Id is required.")
                    .NotEqual(Guid.Empty).WithMessage("Invalid Basket item ID.");





        }

    }
}
