using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Product.Dtos;
using PrimeShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Product.Commands
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Guid>
    {
        private readonly IRepository<Domain.Entities.Product> _categoryRepo;

        public DeleteProductHandler(IRepository<Domain.Entities.Product> categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task<Guid> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {

            var product = await _categoryRepo.GetAsync(request.id);

            if (product != null)
            {
                _categoryRepo.Delete(product);
            }

            await _categoryRepo.UnitOfWork.SaveChangesAsync(cancellationToken);

            return product.Id;

        }
}
}
