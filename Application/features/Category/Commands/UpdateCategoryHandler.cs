using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Category.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Category.Commands
{
    internal class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Guid>
    {

        private readonly IRepository<Domain.Entities.Category> _categoryRepo;

        public UpdateCategoryHandler(IRepository<Domain.Entities.Category> categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task<Guid> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepo.GetAsync(request.Id);

            if(category != null)
            {
                category.Name = request.Name;
                category.Description = request.Description;
            }
         
            _categoryRepo.Update(category);

            await _categoryRepo.UnitOfWork.SaveChangesAsync(cancellationToken);
            return category.Id;
        }

    }
}
