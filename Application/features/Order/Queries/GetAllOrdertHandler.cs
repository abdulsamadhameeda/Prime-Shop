using MediatR;
using Microsoft.EntityFrameworkCore;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Order.Dtos;


namespace PrimeShop.Application.features.Order.Queries
{
    internal class GetAllOrdertHandler : IRequestHandler<GetAllOrderQueries, List<OrderDto>>
    {

        private readonly IRepository<Domain.Entities.Order> _OrderRepo;

        public GetAllOrdertHandler(IRepository<Domain.Entities.Order> OrderRepo)
        {
            _OrderRepo = OrderRepo;
        }
        public async Task<List<OrderDto>> Handle(GetAllOrderQueries request, CancellationToken cancellationToken)
        {
            var order = await _OrderRepo.GetAll()
                .Select(x => new OrderDto
                {
                    Id = x.Id,
                    Status = x.Status,
                    CustomerId = x.CustomerId,
                    OrderDate = x.OrderDate,
                    ShippingCity = x.ShippingCity,
                    ShippingStreet = x.ShippingStreet,
                    TotalAmount = x.TotalAmount
                }).ToListAsync();


            return order;
        }

    }
}
