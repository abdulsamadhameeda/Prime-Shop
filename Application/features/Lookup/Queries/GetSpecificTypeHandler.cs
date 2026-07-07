using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Lookup.Dtos;

namespace PrimeShop.Application.features.Lookup.Queries
{
    internal class GetSpecificTypeHandler : IRequestHandler<GetSpecificTypeQueries, List<LookupDto>>
    {

        private readonly IRepository<Domain.Entities.Lookup> _LookupRepo;

        public GetSpecificTypeHandler(IRepository<Domain.Entities.Lookup> LookupRepo)
        {
            _LookupRepo = LookupRepo;
        }

        public async Task<List<LookupDto>> Handle(GetSpecificTypeQueries request, CancellationToken cancellationToken)
        {

            string filePath = Path.Combine(AppContext.BaseDirectory, "SQL", "GetSpecificType.sql");

            string sqlQuery = await File.ReadAllTextAsync(filePath);

            var result = await _LookupRepo.QuerySql<LookupDto>(sqlQuery, new { Type = request.type });

            return result.ToList();
        }
            
    }
}
