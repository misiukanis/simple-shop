using Shop.Shared.Enums;

namespace Shop.Shared.DTOs
{
    public class OrderDTO
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }        

        public string CustomerName { get; set; } = default!;

        public string City { get; set; } = default!;

        public string Street { get; set; } = default!;

        public OrderStatus OrderStatus { get; set; }

        public DateTime CreationDate { get; set; }

        public List<OrderItemDTO> OrderItems { get; set; } = default!;

        public decimal TotalPrice { get; set; }

        public int TotalQuantity { get; set; }
    }

    public class OrderItemDTO
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; } = default!;

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
