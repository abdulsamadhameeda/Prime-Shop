using FluentValidation;
using PrimeShop.Application.features.Basket.Dtos;


namespace PrimeShop.Application.features.Basket.Validatords
{
    internal class UpdateBasketCommandValidators : AbstractValidator<UpdateBasketCommand>
    {
        public UpdateBasketCommandValidators()
        {
            RuleFor(c => c.Id)
                 .NotEmpty().WithMessage("Basket Id is required.")
                 .NotEqual(Guid.Empty).WithMessage("Invalid Basket ID.");

            RuleFor(c => c.CreatedAt)
            .NotEmpty().WithMessage("This feild is requird")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Creation date cannot be in the future.");

            RuleFor(c => c.UserId).NotEmpty().WithMessage("Customer Id is requird");


        }
    }

}
