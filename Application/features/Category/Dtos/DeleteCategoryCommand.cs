using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Category.Dtos
{

    public record DeleteCategoryCommand
   (
       Guid Id
    
   ) : IRequest<Guid>;
}
