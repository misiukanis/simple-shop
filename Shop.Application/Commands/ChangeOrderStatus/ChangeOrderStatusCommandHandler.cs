using MediatR;
using Shop.Application.Exceptions;
using Shop.Domain.Aggregates.OrderAggregate;
using Shop.Domain.Dispatchers.Interfaces;
using Shop.Domain.Repositories.Interfaces;

namespace Shop.Application.Commands.ChangeOrderStatus
{
    public class ChangeOrderStatusCommandHandler : IRequestHandler<ChangeOrderStatusCommand>
    {
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly IEventStoreRepository _eventStoreRepository;

        public ChangeOrderStatusCommandHandler(
            IDomainEventDispatcher domainEventDispatcher,
            IEventStoreRepository eventStoreRepository)
        {
            _domainEventDispatcher = domainEventDispatcher;
            _eventStoreRepository = eventStoreRepository;
        }

        public async Task<Unit> Handle(ChangeOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _eventStoreRepository.LoadAsync<Order>(request.Id);
            if (order == null)
            {
                throw new NotFoundException(nameof(Order), request.Id);
            }

            switch ((OrderStatus)request.OrderStatus)
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

            return Unit.Value;
        }
    }
}
