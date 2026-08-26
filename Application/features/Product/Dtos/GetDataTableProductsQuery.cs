using MediatR;
using PrimeShop.Application.Common.Models;
using PrimeShop.Application.features.Order.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Product.Dtos
{
    public class GetDataTableProductsQuery : IRequest<DataTableResponse<ProductDto>>
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public string Search { get; set; }
    }

   

}

