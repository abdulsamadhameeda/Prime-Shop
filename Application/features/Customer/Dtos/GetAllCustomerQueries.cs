using MediatR;
using PrimeShop.Application.features.Product.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Customer.Dtos
{
    public class GetAllCustomerQueries : IRequest<List<CustomerDto>>
    {
    }
}
