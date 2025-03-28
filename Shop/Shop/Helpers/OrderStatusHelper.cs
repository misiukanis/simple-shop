using Shop.Domain.Aggregates.OrderAggregate;

namespace Shop.Helpers
{
    public static class OrderStatusHelper
    {
        public static string GetDescription(OrderStatus status) => status switch
        {
            OrderStatus.New => "New",
            OrderStatus.Paid => "Paid",
            OrderStatus.Shipped => "Shipped",
            OrderStatus.Cancelled => "Cancelled",
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
    }
}
