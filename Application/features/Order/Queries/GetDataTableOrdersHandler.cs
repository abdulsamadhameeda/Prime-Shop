using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Order.Dtos;


namespace PrimeShop.Application.features.Order.Queries
{
    internal class GetDataTableOrdersHandler : IRequestHandler<GetDataTableOrdersQuery, DataTableResponse<OrderDto>>
    {
        private readonly IRepository<Domain.Entities.Order> _OrderRepo;

        public GetDataTableOrdersHandler(IRepository<Domain.Entities.Order> OrderRepo)
        {
            _OrderRepo = OrderRepo;
        }

        public async Task<DataTableResponse<OrderDto>> Handle(GetDataTableOrdersQuery request, CancellationToken cancellationToken)
        {

            var query = _OrderRepo.GetAll().AsQueryable();
            int recordsTotal = query.Count();

            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(o => o.Status.ToString().Contains(request.Search) ||
                          o.ShippingStreet.Contains(request.Search) ||
                          o.ShippingCityLookup.Value.Contains(request.Search) || 
                          o.Id.ToString().Contains(request.Search));
            }

            int recordsFiltered = query.Count();

            var data = query
           .Skip(request.Start)
           .Take(request.Length)
           .Select(x => new OrderDto
           {
               Id = x.Id,
               OrderDate = x.OrderDate,
               TotalAmount = x.TotalAmount,
               Status = x.Status, 
               ShippingStreet = x.ShippingStreet,
               ShippingCity = x.ShippingCityLookup.Value, 
               CustomerId = x.CustomerId 
           })
           .ToList();

            return new DataTableResponse<OrderDto>
            {
                Draw = request.Draw,
                RecordsTotal = recordsTotal,
                RecordsFiltered = recordsFiltered,
                Data = data
            };
        }

    }
}
