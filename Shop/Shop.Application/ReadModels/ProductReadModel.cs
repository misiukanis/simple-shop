using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Shop.Application.ReadModels
{
    public class ProductReadModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }
    }
}
