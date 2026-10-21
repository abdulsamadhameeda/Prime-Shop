using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Wishlist.Dtos
{
    public record DeleteWishlistCommand(Guid Id, string UserId)
    : IRequest<bool>;
}
