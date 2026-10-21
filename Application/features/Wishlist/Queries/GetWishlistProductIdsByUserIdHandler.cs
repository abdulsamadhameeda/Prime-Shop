using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Wishlist.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Wishlist.Queries
{
    public class GetWishlistProductIdsByUserIdHandler : IRequestHandler<GetWishlistProductIdsByUserIdQueries, List<WishlistItemDto>>
    {

        private readonly IRepository<Domain.Entities.Wishlist> _wishlistRepo;

        public GetWishlistProductIdsByUserIdHandler(
            IRepository<Domain.Entities.Wishlist> wishlistRepo)
        {
            _wishlistRepo = wishlistRepo;
        }


        public async Task<List<WishlistItemDto>> Handle(
           GetWishlistProductIdsByUserIdQueries request,
           CancellationToken cancellationToken)
        {
            string filePath = Path.Combine(
                AppContext.BaseDirectory,
                "SQL",
                "GetWishlistProductIdsByUserId.sql");

            string sqlQuery = await File.ReadAllTextAsync(filePath);

            var result = await _wishlistRepo.QuerySql<WishlistItemDto>(
                sqlQuery,
                new { UserId = request.UserId });

            return result.ToList();
        }



    }
}
