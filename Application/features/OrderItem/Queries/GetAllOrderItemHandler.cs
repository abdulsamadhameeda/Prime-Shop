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
            string filePath = Path.Combine(AppContext.BaseDirectory, "SQL", "GetAllOrderItem.sql");

            string sqlQuery = await File.ReadAllTextAsync(filePath);

            var result = await _OrderItemRepo.QuerySql<OrderItemDto>(sqlQuery , new { OrderId = request.orderId });

            return result.ToList();
        }


    }
}
