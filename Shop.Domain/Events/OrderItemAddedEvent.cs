using MediatR;

namespace Shop.Domain.Events
{
    public class OrderItemAddedEvent : INotification
    {
        public Guid OrderId { get; }
        public Guid ProductId { get; }
        public int Quantity { get; }
        public decimal Price { get; }

        public OrderItemAddedEvent(Guid orderId, Guid productId, int quantity, decimal price)
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }
    }
}
