using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Basket.Dtos;
using PrimeShop.Application.features.Order.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Order.Commands
{
    internal class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Guid>
    {

        private readonly IRepository<Domain.Entities.Order> _OrderRepo;

        public CreateOrderHandler(IRepository<Domain.Entities.Order> OrderRepo)
        {
            _OrderRepo = OrderRepo;
        }
        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var Order = new Domain.Entities.Order
            {
                OrderDate = request.OrderDate,
                TotalAmount = request.TotalAmount,
                Status = request.Status,
                ShippingStreet = request.ShippingStreet,
                ShippingCityId = request.ShippingCityId,
                UserId = request.UserId

            };

            _OrderRepo.Add(Order);

            await _OrderRepo.UnitOfWork.SaveChangesAsync(cancellationToken);

            return Order.Id;
        }


    }
}
