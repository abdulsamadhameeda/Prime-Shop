using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Order.Dtos;

namespace PrimeShop.Application.features.Order.Queries
{
    internal class GetByIdOrdertHandler : IRequestHandler<GetByIdOrderQueries, OrderDto>
    {

        private readonly IRepository<Domain.Entities.Order> _OrderRepo;

        public GetByIdOrdertHandler(IRepository<Domain.Entities.Order> OrderRepo)
        {
            _OrderRepo = OrderRepo;
        }
        public async Task<OrderDto> Handle(GetByIdOrderQueries request, CancellationToken cancellationToken)
        {
            var order = await _OrderRepo.GetAsync(request.id);
            return new OrderDto
            {
                Id = order.Id,
                TotalAmount = order.TotalAmount,
                ShippingCity = order.ShippingCity,
                CustomerId = order.CustomerId,
                ShippingStreet = order.ShippingStreet,
                OrderDate = order.OrderDate,
                Status = order.Status
            };
        }

    }
}
