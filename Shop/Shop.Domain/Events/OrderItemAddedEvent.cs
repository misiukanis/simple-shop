using Shop.Domain.Core;

namespace Shop.Domain.Events
{
    public class OrderItemAddedEvent(
        Guid orderId, 
        Guid productId, 
        int quantity, 
        decimal price) : DomainEvent
    {
        public Guid OrderId { get; } = orderId;
        public Guid ProductId { get; } = productId;
        public int Quantity { get; } = quantity;
        public decimal Price { get; } = price;
    }
}
