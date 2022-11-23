namespace Shop.Domain.Aggregates.OrderAggregate
{
    public enum OrderStatus
    {
        New = 1,
        Paid = 2,
        Shipped = 3,
        Cancelled = 4
    }
}
