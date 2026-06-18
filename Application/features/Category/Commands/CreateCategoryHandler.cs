using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Category.Dtos;


namespace PrimeShop.Application.features.Category.Commands;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Guid>
{

    private readonly IRepository<Domain.Entities.Category> _categoryRepo;

    public CreateCategoryHandler(IRepository<Domain.Entities.Category> categoryRepo)
    {
        _categoryRepo = categoryRepo;
    }
    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Domain.Entities.Category
        {
            Name = request.Name,
            Description = request.Description

        };

        _categoryRepo.Add(category);

        await _categoryRepo.UnitOfWork.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}

