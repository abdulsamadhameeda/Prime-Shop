using MediatR;
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

    public class DataTableResponse<T>
    {
        public int Draw { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public List<T> Data { get; set; }
    }

}

