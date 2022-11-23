namespace Shop.Shared.DTOs
{
    public class ProductDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public decimal Price { get; set; }
    }
}
