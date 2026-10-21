using FluentValidation;
using PrimeShop.Application.features.Order.Dtos;


namespace PrimeShop.Application.features.Order.Validatords
{
    public class UpdateOrderCommandValidators : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidators()
        {
            RuleFor(c => c.OrderDate)
     .NotEmpty().WithMessage("Order date is required.")
     .LessThanOrEqualTo(DateTime.Now).WithMessage("Order date cannot be in the future.");

            RuleFor(c => c.TotalAmount)
    .GreaterThan(0).WithMessage("Total amount must be greater than zero.");

            RuleFor(c => c.Status)
    .IsInEnum().WithMessage("Invalid order status value.");

            RuleFor(c => c.ShippingStreet)
    .NotEmpty().WithMessage("Shipping street address is required.")
    .MaximumLength(200).WithMessage("Shipping street cannot exceed 200 characters.");

            RuleFor(c => c.ShippingCityId)
       .NotEmpty().WithMessage("Shipping city is required.")
       .NotEqual(Guid.Empty).WithMessage("Please select a valid city.");

            RuleFor(c => c.UserId)
    .NotEmpty().WithMessage("Customer ID is required.");
        }
    }
}
