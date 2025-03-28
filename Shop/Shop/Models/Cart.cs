namespace Shop.Models
{
    public class Cart
    {
        public List<CartItem> CartItems { get; set; } = new();
        public int TotalQuantity => CartItems.Sum(x => x.Quantity);
        public decimal TotalPrice => CartItems.Sum(x => x.Price * x.Quantity);
    }

    public class CartItem
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
