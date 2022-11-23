using Shop.Domain.Core;

namespace Shop.Domain.Dispatchers.Interfaces
{
    public interface IDomainEventDispatcher
    {
        Task DispatchEventsAsync<T>(T aggregate) where T : AggregateRoot;
    }
}
