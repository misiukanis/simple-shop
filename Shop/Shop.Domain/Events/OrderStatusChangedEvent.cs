using Shop.Domain.Aggregates.OrderAggregate;
using Shop.Domain.Core;

namespace Shop.Domain.Events
{
    public class OrderStatusChangedEvent(
        Guid id, 
        OrderStatus orderStatus) : DomainEvent
    {
        public Guid Id { get; } = id;
        public OrderStatus OrderStatus { get; } = orderStatus;
    }
}
