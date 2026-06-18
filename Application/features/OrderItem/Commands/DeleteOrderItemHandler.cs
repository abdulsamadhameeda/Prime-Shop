using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Order.Dtos;
using PrimeShop.Application.features.OrderItem.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.OrderItem.Commands
{
    internal class DeleteOrderItemHandler : IRequestHandler<DeleteOrderItemCommand, Guid>
    {

        private readonly IRepository<Domain.Entities.OrderItem> _OrderItemRepo;

        public DeleteOrderItemHandler(IRepository<Domain.Entities.OrderItem> OrderItemRepo)
        {
            _OrderItemRepo = OrderItemRepo;
        }
        public async Task<Guid> Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
        {
            var orderItem = await _OrderItemRepo.GetAsync(request.id);

            if (orderItem != null)
            {
                _OrderItemRepo.Delete(orderItem);
            }

            await _OrderItemRepo.UnitOfWork.SaveChangesAsync(cancellationToken);
            return orderItem.Id;
        }



    }
}
