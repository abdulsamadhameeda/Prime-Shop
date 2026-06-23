using FluentValidation;
using PrimeShop.Application.features.OrderItem.Dtos;


namespace PrimeShop.Application.features.OrderItem.Validatords
{
    public class CreateOrderItemCommandValidators : AbstractValidator<CreateOrderItemCommand>
    {
        public CreateOrderItemCommandValidators()
        {

            RuleFor(c => c.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

            RuleFor(c => c.UnitPrice)
            .GreaterThan(0).WithMessage("Unit price must be greater than zero.");

            RuleFor(c => c.ProductId)
                .NotEmpty().WithMessage("Product ID is required.")
                .NotEqual(Guid.Empty).WithMessage("The provided Product ID is invalid.");

            RuleFor(c => c.OrderId)
                .NotEmpty().WithMessage("Order ID is required.")
                .NotEqual(Guid.Empty).WithMessage("The provided Order ID is invalid.");

        }
    }
}
