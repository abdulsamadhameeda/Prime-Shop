using MediatR;

namespace PrimeShop.Application.features.Lookup.Dtos
{
    public record GetSpecificTypeQueries(string type) : IRequest<List<LookupDto>>
    {
    }
}
