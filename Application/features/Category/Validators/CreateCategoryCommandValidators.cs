using FluentValidation;
using PrimeShop.Application.features.Category.Dtos;


namespace PrimeShop.Application.features.Category.Validatords
{
    public class CreateCategoryCommandValidators : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidators()
        {

            RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(3, 100).WithMessage("Name must be between 3 and 100 characters.");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

        }
    }
}
