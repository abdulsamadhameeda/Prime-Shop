using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.Common.Models;
using PrimeShop.Application.features.Product.Dtos;


namespace PrimeShop.Application.features.Product.Queries
{
    public class GetDataTableProductsHandler : IRequestHandler<GetDataTableProductsQuery, DataTableResponse<ProductDto>>
    {
        private readonly IRepository<Domain.Entities.Product> _ProductRepo;

        public GetDataTableProductsHandler(IRepository<Domain.Entities.Product> ProductRepo)
        {
            _ProductRepo = ProductRepo;
        }

        public async Task<DataTableResponse<ProductDto>> Handle(GetDataTableProductsQuery request, CancellationToken cancellationToken)
        {

            var query = _ProductRepo.GetAll().AsQueryable();
            int recordsTotal = query.Count();

            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(x =>
                x.Name.Contains(request.Search) ||
                x.Description.Contains(request.Search) ||
                x.Category.Name.Contains(request.Search) ||
                x.StockQuantity.ToString().Contains(request.Search)||
                x.Price.ToString().Contains(request.Search));
            }

            int recordsFiltered = query.Count();

            var data = query
           .Skip(request.Start)
           .Take(request.Length)
           .Select(x => new ProductDto
           {
               Id = x.Id,
               Description = x.Description,
               Name = x.Name,
               ImageUrl = x.ImageUrl,
               Price = x.Price,
               StockQuantity = x.StockQuantity,
               CategoryName = x.Category.Name
           })
           .ToList();

            return new DataTableResponse<ProductDto>
            {
                Draw = request.Draw,
                RecordsTotal = recordsTotal,
                RecordsFiltered = recordsFiltered,
                Data = data
            };
        }

    }
}
