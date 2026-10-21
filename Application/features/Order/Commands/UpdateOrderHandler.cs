using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Order.Dtos;


namespace PrimeShop.Application.features.Order.Commands
{
    internal class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, Guid>
    {

        private readonly IRepository<Domain.Entities.Order> _OrderRepo;

        public UpdateOrderHandler(IRepository<Domain.Entities.Order> OrderRepo)
        {
            _OrderRepo = OrderRepo;
        }

        public async Task<Guid> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _OrderRepo.GetAsync(request.Id);

            if (order == null)
            {
                throw new Exception("Order not found");
            }


            order.UserId = request.UserId;
            order.ShippingStreet = request.ShippingStreet;
            order.ShippingCityId = request.ShippingCityId;
            order.Status = request.Status;
            order.TotalAmount = request.TotalAmount;
            order.OrderDate = request.OrderDate;

            _OrderRepo.Update(order);

            await _OrderRepo.UnitOfWork.SaveChangesAsync(cancellationToken);
            return order.Id;
        }

    }
}
