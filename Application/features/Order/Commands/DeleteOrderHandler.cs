using MediatR;
using PrimeShop.Application.Common.Interfaces;
using PrimeShop.Application.features.Order.Dtos;


namespace PrimeShop.Application.features.Order.Commands
{
    internal class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, bool>
    {

        private readonly IRepository<Domain.Entities.Order> _OrderRepo;

        public DeleteOrderHandler(IRepository<Domain.Entities.Order> OrderRepo)
        {
            _OrderRepo = OrderRepo;
        }
        public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _OrderRepo.GetAsync(request.Id);

            if (order == null)
            {
                throw new Exception();
            }
            _OrderRepo.Delete(order);

            await _OrderRepo.UnitOfWork.SaveChangesAsync();
            return true;
        }

    }
}
