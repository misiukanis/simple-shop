using Shop.Domain.Core;

namespace Shop.Domain.Repositories.Interfaces
{
    public interface IEventStoreRepository
    {
        Task<T> LoadAsync<T>(Guid aggregateId) where T : AggregateRoot, new();
        Task SaveAsync<T>(T aggregate) where T : AggregateRoot, new();
    }
}
