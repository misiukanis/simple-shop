using MediatR;
using Shop.Domain.Aggregates.OrderAggregate;
using Shop.Domain.Aggregates.ProductAggregate;
using Shop.Domain.Interfaces.Dispatchers;
using Shop.Domain.Interfaces.Repositories;

namespace Shop.Application.Commands.CreateOrder
{
    public class CreateOrderCommandHandler(
        IDomainEventDispatcher domainEventDispatcher,
        IEventStoreRepository eventStoreRepository) : IRequestHandler<CreateOrderCommand>
    {
        private readonly IDomainEventDispatcher _domainEventDispatcher = domainEventDispatcher;
        private readonly IEventStoreRepository _eventStoreRepository = eventStoreRepository;

        public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var address = new Address(request.City, request.Street);
            var order = new Order(request.Id, request.CustomerId, address);

            foreach (var item in request.OrderItems)
            {
                var product = await _eventStoreRepository.LoadAsync<Product>(item.ProductId);

                order.AddOrderItem(item.ProductId, item.Quantity, product.Price);
            }

            await _eventStoreRepository.SaveAsync(order);
            await _domainEventDispatcher.DispatchEventsAsync(order);
        }
    }
}
