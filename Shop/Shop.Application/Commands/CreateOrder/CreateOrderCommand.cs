using MediatR;

namespace Shop.Application.Commands.CreateOrder
{
    public class CreateOrderCommand(
        Guid id, 
        Guid customerId, 
        string city, 
        string street,
        List<CreateOrderItemCommand> orderItems) : IRequest
    {
        public Guid Id { get; } = id;
        public Guid CustomerId { get; } = customerId;
        public string City { get; } = city;
        public string Street { get; } = street;
        public List<CreateOrderItemCommand> OrderItems { get; } = orderItems;
    }

    public class CreateOrderItemCommand(
        Guid productId, 
        int quantity)
    {
        public Guid ProductId { get; set; } = productId;
        public int Quantity { get; set; } = quantity;
    }
}
