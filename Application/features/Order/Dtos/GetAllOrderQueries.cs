using MediatR;

namespace PrimeShop.Application.features.Order.Dtos
{

    public record GetAllOrderQueries(int? status ) : IRequest<List<OrderDto>>;

}
