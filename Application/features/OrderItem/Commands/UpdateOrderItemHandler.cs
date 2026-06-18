using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.OrderItem.Dtos;


namespace PrimeShop.Application.features.OrderItem.Commands
{
    internal class UpdateOrderItemHandler : IRequestHandler<UpdateOrderItemCommand, Guid>
    {

        private readonly IRepository<Domain.Entities.OrderItem> _OrderItemRepo;

        public UpdateOrderItemHandler(IRepository<Domain.Entities.OrderItem> OrderItemRepo)
        {
            _OrderItemRepo = OrderItemRepo;
        }

        public async Task<Guid> Handle(UpdateOrderItemCommand request, CancellationToken cancellationToken)
        {
            var orderItem = await _OrderItemRepo.GetAsync(request.id);

            if (orderItem != null)
            {
                orderItem.OrderId = request.OrderId;
                orderItem.ProductId = request.ProductId;
                orderItem.UnitPrice = request.UnitPrice;
                orderItem.Quantity = request.Quantity;

            }

            _OrderItemRepo.Update(orderItem);

            await _OrderItemRepo.UnitOfWork.SaveChangesAsync(cancellationToken);
            return orderItem.Id;
        }

    }
}
