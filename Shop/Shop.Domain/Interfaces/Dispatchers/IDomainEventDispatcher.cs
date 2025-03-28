using Shop.Domain.Core;

namespace Shop.Domain.Interfaces.Dispatchers
{
    public interface IDomainEventDispatcher
    {
        Task DispatchEventsAsync<T>(T aggregate) where T : AggregateRoot;
    }
}
