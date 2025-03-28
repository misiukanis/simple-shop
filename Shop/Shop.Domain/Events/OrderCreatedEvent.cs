using Shop.Domain.Aggregates.OrderAggregate;
using Shop.Domain.Core;

namespace Shop.Domain.Events
{
    public class OrderCreatedEvent(
        Guid id, 
        Guid customerId, 
        OrderStatus orderStatus,
        Address address, 
        DateTime creationDate) : DomainEvent
    {
        public Guid Id { get; } = id;
        public Guid CustomerId { get; } = customerId;
        public OrderStatus OrderStatus { get; } = orderStatus;
        public Address Address { get; } = address;
        public DateTime CreationDate { get; } = creationDate;
    }
}
