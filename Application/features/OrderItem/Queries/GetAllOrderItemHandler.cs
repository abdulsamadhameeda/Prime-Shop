using MediatR;
using Microsoft.EntityFrameworkCore;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.OrderItem.Dtos;


namespace PrimeShop.Application.features.OrderItem.Queries
{
    internal class GetAllOrderItemHandler : IRequestHandler<GetAllOrderItemQueries, List<OrderItemDto>>
    {

        private readonly IRepository<Domain.Entities.OrderItem> _OrderItemRepo;

        public GetAllOrderItemHandler(IRepository<Domain.Entities.OrderItem> OrderItemRepo)
        {
            _OrderItemRepo = OrderItemRepo;
        }
        public async Task<List<OrderItemDto>> Handle(GetAllOrderItemQueries request, CancellationToken cancellationToken)
        {
            var orderItem = await _OrderItemRepo.GetAll()
                .Select(x => new OrderItemDto
                {
                    Id = x.Id,
                  OrderId = x.OrderId,
                  ProductId = x.ProductId,
                  Quantity = x.Quantity,
                  UnitPrice=x.UnitPrice
                }).ToListAsync();


            return orderItem;
        }


    }
}
