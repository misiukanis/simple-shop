using System.ComponentModel.DataAnnotations;

namespace Shop.Models.Requests
{
    public class CreateCustomerRequest
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = default!;
    }
}
