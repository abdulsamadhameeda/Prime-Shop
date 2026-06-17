using MediatR;
using PrimeShop.Application.Common.Interfaces;


namespace PrimeShop.Application.features.Category.Commands.CreateCategory;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Guid>
{

    private readonly IApplicationDbContext _DbContext;
    public CreateCategoryHandler(IApplicationDbContext context)
    {
        _DbContext = context;
    }
    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Domain.Entities.Category
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description

        };

        _DbContext.Categories.Add(category);

        await _DbContext.SaveChangesAsync(cancellationToken);
      
        return category.Id;
    }
}

