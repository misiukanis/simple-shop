using MediatR;

namespace Shop.Application.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }
        public string City { get; } = default!;
        public string Street { get; } = default!;
        public List<CreateOrderItemCommand> OrderItems { get; }

        public CreateOrderCommand(Guid id, Guid customerId, string city, string street,
            List<CreateOrderItemCommand> orderItems)
        {
            Id = id;
            CustomerId = customerId;
            City = city;
            Street = street;
            OrderItems = orderItems;
        }
    }

    public class CreateOrderItemCommand
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public CreateOrderItemCommand(Guid productId, int quantity, decimal price)
        {
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }
    }
}
