using System.ComponentModel.DataAnnotations;

namespace Shop.Models.Requests
{
    public class UpdateCustomerRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = default!;
    }
}
