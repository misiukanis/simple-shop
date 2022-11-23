using System.ComponentModel.DataAnnotations;

namespace Shop.Shared.DTOs
{
    public class CreateCustomerDTO
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = default!;
    }
}
