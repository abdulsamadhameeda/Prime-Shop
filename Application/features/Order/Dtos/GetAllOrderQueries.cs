using MediatR;

namespace PrimeShop.Application.features.Order.Dtos
{

    public class GetAllOrderQueries : IRequest<List<OrderDto>>;

}
