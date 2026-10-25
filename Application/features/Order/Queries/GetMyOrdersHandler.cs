using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.Common.Models;
using PrimeShop.Application.features.Order.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Order.Queries
{
    internal class GetMyOrdersHandler
           : IRequestHandler<GetMyOrdersQuery, DataTableResponse<OrderDto>>
    {
        private readonly IRepository<Domain.Entities.Order> _orderRepo;

        public GetMyOrdersHandler(IRepository<Domain.Entities.Order> orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public async Task<DataTableResponse<OrderDto>> Handle(
            GetMyOrdersQuery request,
            CancellationToken cancellationToken)
        {
            var sqlFolder = Path.Combine(AppContext.BaseDirectory, "SQL");

            var ordersSql = await File.ReadAllTextAsync(
                Path.Combine(sqlFolder, "GetMyOrders.sql"));

            var totalCountSql = await File.ReadAllTextAsync(
                Path.Combine(sqlFolder, "GetMyOrdersTotalCount.sql"));

            var filteredCountSql = await File.ReadAllTextAsync(
                Path.Combine(sqlFolder, "GetMyOrdersFilteredCount.sql"));

            var parameters = new
            {
                request.UserId,
                request.Status,
                request.FromDate,
                request.ToDate,
                request.Start,
                request.Length
            };

            var data = (await _orderRepo.QuerySql<OrderDto>(
                ordersSql,
                parameters)).ToList();

            var recordsTotal = (await _orderRepo.QuerySql<int>(
                totalCountSql,
                new { request.UserId })).FirstOrDefault();

            var recordsFiltered = (await _orderRepo.QuerySql<int>(
                filteredCountSql,
                new
                {
                    request.UserId,
                    request.Status,
                    request.FromDate,
                    request.ToDate
                })).FirstOrDefault();

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
