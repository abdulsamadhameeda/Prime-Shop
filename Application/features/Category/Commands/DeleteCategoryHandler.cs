using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Category.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Category.Commands
{
    internal class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {

        private readonly IRepository<Domain.Entities.Category> _categoryRepo;

        public DeleteCategoryHandler(IRepository<Domain.Entities.Category> categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }


        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepo.GetAsync(request.Id);

            if (category == null)
            {
                throw new Exception();
            }
            _categoryRepo.Delete(category);

            await _categoryRepo.UnitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
