using System.ComponentModel.DataAnnotations;

namespace Shop.Models.Requests
{
    public class CreateProductRequest
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = default!;

        [Range(1, 1000)]
        public decimal Price { get; set; }
    }
}
