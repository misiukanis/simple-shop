using Shop.Domain.Core;

namespace Shop.Domain.Events
{
    public class CustomerCreatedEvent(
        Guid id, 
        string name) : DomainEvent
    {
        public Guid Id { get; } = id;
        public string Name { get; } = name;
    }
}
