using Shop.Domain.Core;

namespace Shop.Domain.Events
{
    public class ProductDeletedEvent(
        Guid id) : DomainEvent
    {
        public Guid Id { get; } = id;
    }
}
