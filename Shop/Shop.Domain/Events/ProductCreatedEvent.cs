using Shop.Domain.Core;

namespace Shop.Domain.Events
{
    public class ProductCreatedEvent(
        Guid id, 
        string name, 
        decimal price) : DomainEvent
    {
        public Guid Id { get; } = id;
        public string Name { get; } = name;
        public decimal Price { get; } = price;
    }
}
