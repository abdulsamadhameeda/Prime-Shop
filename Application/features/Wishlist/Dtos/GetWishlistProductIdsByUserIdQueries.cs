using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Wishlist.Dtos
{
    public record GetWishlistProductIdsByUserIdQueries(string UserId)
     : IRequest<List<WishlistItemDto>>;
}
