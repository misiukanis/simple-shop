using System.ComponentModel.DataAnnotations;

namespace Shop.Shared.DTOs
{
    public class UpdateCustomerDTO
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = default!;
    }
}
