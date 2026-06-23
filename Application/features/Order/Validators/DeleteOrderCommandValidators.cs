using FluentValidation;
using PrimeShop.Application.features.Order.Dtos;

namespace PrimeShop.Application.features.Order.Validatords
{
    public class DeleteOrderCommandValidators : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidators()
        {

            RuleFor(c => c.Id)
                      .NotEmpty().WithMessage("Order Id is required.")
                      .NotEqual(Guid.Empty).WithMessage("Invalid Order ID.");

        }
    }
}
