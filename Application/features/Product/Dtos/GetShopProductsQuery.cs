using MediatR;
using PrimeShop.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Product.Dtos
{
    public class GetShopProductsQuery
      : IRequest<DataTableResponse<ProductDto>>
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }

        public Guid? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        public string SortBy { get; set; } = "default";
    }
}
