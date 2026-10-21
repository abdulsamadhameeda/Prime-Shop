using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Product.Dtos;
using PrimeShop.Application.features.Wishlist.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Wishlist.Commands
{
    internal class AddToWishlistHandler : IRequestHandler<AddToWishlistCommand, Guid>
    {

        private readonly IRepository<Domain.Entities.Wishlist> _wishlistRepo;

        public AddToWishlistHandler(IRepository<Domain.Entities.Wishlist> wishlistRepo)
        {
            _wishlistRepo = wishlistRepo;
        }


        public async Task<Guid> Handle(AddToWishlistCommand request, CancellationToken cancellationToken)
        {
            var wishlistItem = new Domain.Entities.Wishlist
            {
                ProductId = request.ProductId,
                UserId = request.UserId
            };

            _wishlistRepo.Add(wishlistItem);

            await _wishlistRepo.UnitOfWork.SaveChangesAsync(cancellationToken);

            return wishlistItem.Id;


        }


    }
}
