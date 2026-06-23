using FluentValidation;
using PrimeShop.Application.features.Basket.Dtos;


namespace PrimeShop.Application.features.Basket.Validatords
{
    public class DeleteBasketCommandValidators : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommandValidators()
        {
            RuleFor(c => c.Id)
                    .NotEmpty().WithMessage("Basket Id is required.")
                    .NotEqual(Guid.Empty).WithMessage("Invalid Basket ID.");





        }

    }
}
