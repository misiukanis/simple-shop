using System.ComponentModel.DataAnnotations;

namespace Shop.Models.Requests
{
    public class CreateOrderRequest
    {
        [Display(Name = "Customer")]
        [Required]
        public Guid? CustomerId { get; set; }

        [Required]
        [StringLength(100)]
        public string City { get; set; } = default!;

        [Required]
        [StringLength(100)]
        public string Street { get; set; } = default!;

        [MinLength(1)]
        public List<CreateOrderItemRequest> OrderItems { get; set; } = new();
    }

    public class CreateOrderItemRequest
    {
        [Required]
        public Guid ProductId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
