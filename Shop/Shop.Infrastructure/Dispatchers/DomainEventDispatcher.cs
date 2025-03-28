using MediatR;
using Shop.Domain.Core;
using Shop.Domain.Interfaces.Dispatchers;

namespace Shop.Infrastructure.Services
{
    public class DomainEventDispatcher(IPublisher mediator) : IDomainEventDispatcher
    {
        private readonly IPublisher _mediator = mediator;

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
