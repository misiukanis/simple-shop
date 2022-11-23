using MediatR;
using Shop.Domain.Aggregates.OrderAggregate;
using Shop.Domain.Dispatchers.Interfaces;
using Shop.Domain.Repositories.Interfaces;

namespace Shop.Application.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
    {
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly IEventStoreRepository _eventStoreRepository;

        public CreateOrderCommandHandler(
            IDomainEventDispatcher domainEventDispatcher,
            IEventStoreRepository eventStoreRepository)
        {
            _domainEventDispatcher = domainEventDispatcher;
            _eventStoreRepository = eventStoreRepository;
        }

        public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var address = new Address(request.City, request.Street);
            var order = new Order(request.Id, request.CustomerId, address);

            foreach (var item in request.OrderItems)
            {
                order.AddOrderItem(item.ProductId, item.Quantity, item.Price);
            }

            await _eventStoreRepository.SaveAsync(order);
            await _domainEventDispatcher.DispatchEventsAsync(order);

            return Unit.Value;
        }
    }
}
