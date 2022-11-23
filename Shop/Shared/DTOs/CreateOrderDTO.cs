using System.ComponentModel.DataAnnotations;

namespace Shop.Shared.DTOs
{
    public class CreateOrderDTO
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

        public List<CreateOrderItemDTO> OrderItems { get; set; } = default!;
    }

    public class CreateOrderItemDTO
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
