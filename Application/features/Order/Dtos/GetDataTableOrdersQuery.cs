using MediatR;
using PrimeShop.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Order.Dtos
{
    public class GetDataTableOrdersQuery : IRequest<DataTableResponse<OrderDto>>
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public string Search { get; set; }
    }

   

}

