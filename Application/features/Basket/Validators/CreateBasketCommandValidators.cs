using FluentValidation;
using PrimeShop.Application.features.Basket.Dtos;


namespace PrimeShop.Application.features.Basket.Validatords
{
    public class CreateBasketCommandValidators : AbstractValidator<CreateBasketCommand>
    {
        public CreateBasketCommandValidators()
        {

            //RuleFor(c => c.CreatedAt)
            //.NotEmpty().WithMessage("This feild is requird")
            //.LessThanOrEqualTo(DateTime.Now).WithMessage("Creation date cannot be in the future.");

            RuleFor(c => c.CustomerId).NotEmpty().WithMessage("Customer Id is requird");


        }
    }
}
