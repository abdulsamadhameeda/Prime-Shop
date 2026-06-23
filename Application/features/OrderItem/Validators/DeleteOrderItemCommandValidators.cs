using FluentValidation;
using PrimeShop.Application.features.OrderItem.Dtos;

namespace PrimeShop.Application.features.OrderItem.Validators
{
    public class DeleteOrderItemCommandValidators : AbstractValidator<DeleteOrderItemCommand>
    {
        public DeleteOrderItemCommandValidators()
        {
            RuleFor(c => c.Id)
                    .NotEmpty().WithMessage("Order item Id is required.")
                    .NotEqual(Guid.Empty).WithMessage("Invalid Order item ID.");





        }

    }
}
