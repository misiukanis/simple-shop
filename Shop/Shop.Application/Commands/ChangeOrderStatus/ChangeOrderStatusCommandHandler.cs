using MediatR;
using Shop.Application.Exceptions;
using Shop.Domain.Aggregates.OrderAggregate;
using Shop.Domain.Interfaces.Dispatchers;
using Shop.Domain.Interfaces.Repositories;

namespace Shop.Application.Commands.ChangeOrderStatus
{
    public class ChangeOrderStatusCommandHandler(
        IDomainEventDispatcher domainEventDispatcher,
        IEventStoreRepository eventStoreRepository) : IRequestHandler<ChangeOrderStatusCommand>
    {
        private readonly IDomainEventDispatcher _domainEventDispatcher = domainEventDispatcher;
        private readonly IEventStoreRepository _eventStoreRepository = eventStoreRepository;

        public async Task Handle(ChangeOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _eventStoreRepository.LoadAsync<Order>(request.Id);
            if (order == null)
            {
                throw new NotFoundException(nameof(Order), nameof(Order.Id), request.Id);
            }

            switch (request.OrderStatus)
            {
                case OrderStatus.New:
                    order.SetNewStatus();
                    break;
                case OrderStatus.Paid:
                    order.SetPaidStatus();
                    break;
                case OrderStatus.Shipped:
                    order.SetShippedStatus();
                    break;
                case OrderStatus.Cancelled:
                    order.SetCancelledStatus();
                    break;
            }

            await _eventStoreRepository.SaveAsync(order);
            await _domainEventDispatcher.DispatchEventsAsync(order);
        }
    }
}
