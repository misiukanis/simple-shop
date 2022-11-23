using MediatR;
using Shop.Domain.Aggregates.OrderAggregate;

namespace Shop.Domain.Events
{
    public class OrderStatusChangedEvent : INotification
    {
        public Guid Id { get; }
        public OrderStatus OrderStatus { get; }

        public OrderStatusChangedEvent(Guid id, OrderStatus orderStatus)
        {
            Id = id;
            OrderStatus = orderStatus;
        }
    }
}
