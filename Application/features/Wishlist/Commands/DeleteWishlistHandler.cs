using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Wishlist.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.features.Wishlist.Commands
{
    public class DeleteWishlistHandler : IRequestHandler<DeleteWishlistCommand, bool>
    {
        private readonly IRepository<Domain.Entities.Wishlist> _wishlistRepo;

        public DeleteWishlistHandler(
            IRepository<Domain.Entities.Wishlist> wishlistRepo)
        {
            _wishlistRepo = wishlistRepo;
        }

        public async Task<bool> Handle(
            DeleteWishlistCommand request,
            CancellationToken cancellationToken)
        {
            var wishlistItem = await _wishlistRepo.GetAsync(request.Id);

            if (wishlistItem == null ||
                wishlistItem.UserId != request.UserId)
            {
                return false;
            }

            _wishlistRepo.Delete(wishlistItem);
            await _wishlistRepo.UnitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
