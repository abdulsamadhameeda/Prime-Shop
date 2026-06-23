using FluentValidation;
using PrimeShop.Application.features.Customer.Dtos;

namespace PrimeShop.Application.features.Customer.Validatords
{
    public class DeleteCustomerCommandValidators : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidators()
        {

            RuleFor(c => c.Id)
                      .NotEmpty().WithMessage("Customer Id is required.")
                      .NotEqual(Guid.Empty).WithMessage("Invalid Customer ID.");

        }
    }
}
