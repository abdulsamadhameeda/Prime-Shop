using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.OrderItem.Dtos;


namespace PrimeShop.Application.features.OrderItem.Commands
{
    internal class CreateOrderItemHandler : IRequestHandler<CreateOrderItemCommand, Guid>
    {

        private readonly IRepository<Domain.Entities.OrderItem> _OrderItemRepo;

        public CreateOrderItemHandler(IRepository<Domain.Entities.OrderItem> OrderItemRepo)
        {
            _OrderItemRepo = OrderItemRepo;
        }

        public async Task<Guid> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
        {
            var OrderItem = new Domain.Entities.OrderItem
            {
               OrderId = request.OrderId,
               ProductId = request.ProductId,
               Quantity = request.Quantity,
               UnitPrice = request.UnitPrice,
               

            };

            _OrderItemRepo.Add(OrderItem);

            await _OrderItemRepo.UnitOfWork.SaveChangesAsync(cancellationToken);

            return OrderItem.Id;
        }


    }
}
