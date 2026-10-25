using MediatR;
using PrimeShop.Application.Common.Models;
using PrimeShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Order.Dtos
{
    public class GetMyOrdersQuery : IRequest<DataTableResponse<OrderDto>>
    {
        public string UserId { get; set; } = string.Empty;
        public OrderStatus? Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; } = 12;
    }
}
