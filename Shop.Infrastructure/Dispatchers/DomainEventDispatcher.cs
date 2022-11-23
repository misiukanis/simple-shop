using MediatR;
using Shop.Domain.Core;
using Shop.Domain.Dispatchers.Interfaces;

namespace Shop.Infrastructure.Services
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IPublisher _mediator;

        public DomainEventDispatcher(IPublisher mediator)
        {
            _mediator = mediator;
        }

        public async Task DispatchEventsAsync<T>(T aggregate) where T : AggregateRoot
        {
            foreach (var domainEvent in aggregate.GetDomainEvents())
            {
                await _mediator.Publish(domainEvent);
            }

            aggregate.ClearDomainEvents();
        }
    }
}
