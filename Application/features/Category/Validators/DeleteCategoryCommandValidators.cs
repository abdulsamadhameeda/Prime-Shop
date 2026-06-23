using FluentValidation;
using PrimeShop.Application.features.Category.Dtos;


namespace PrimeShop.Application.features.Category.Validatords
{
    public class DeleteCategoryCommandValidators : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidators()
        {

            RuleFor(c => c.Id)
                      .NotEmpty().WithMessage("Category Id is required.")
                      .NotEqual(Guid.Empty).WithMessage("Invalid Category ID.");

        }
    }
}
