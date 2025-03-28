using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Shop.Domain.Aggregates.OrderAggregate;

namespace Shop.Application.ReadModels
{
    public class OrderReadModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid CustomerId { get; set; }

        public string CustomerName { get; set; } = default!;

        public string City { get; set; } = default!;

        public string Street { get; set; } = default!;

        public OrderStatus OrderStatus { get; set; }

        public DateTime CreationDate { get; set; }

        public List<OrderItemReadModel> OrderItems { get; set; } = new();

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal TotalPrice { get; set; }

        public int TotalQuantity { get; set; }
    }

    public class OrderItemReadModel
    {
        [BsonRepresentation(BsonType.String)]
        public Guid ProductId { get; set; }

        public string ProductName { get; set; } = default!;

        public int Quantity { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }
    }
}
