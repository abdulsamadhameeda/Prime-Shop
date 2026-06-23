using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Product.Dtos;
using PrimeShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Product.Commands
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IRepository<Domain.Entities.Product> _ProductRepo;

        public DeleteProductHandler(IRepository<Domain.Entities.Product> ProductRepo)
        {
            _ProductRepo = ProductRepo;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {

            var product = await _ProductRepo.GetAsync(request.Id);

            //if (product == null)
            //{
            //    throw new Exception();
            //}
            _ProductRepo.Delete(product);

            await _ProductRepo.UnitOfWork.SaveChangesAsync();

            return true;

        }
}
}
