using System.ComponentModel.DataAnnotations;
using static MINIMART.Common.Entities.Attributes.CustomAtrribute;

namespace MINIMART.Common.Entities.Models
{
    public class Address
    {
        public Guid AddressId { get; set; }

        [Required]
        [CDisplayName("Thành phố")]
        public string? City { get; set; }

        [Required]
        [CDisplayName("Quận/Huyện")]
        public string? District { get; set; }

        [Required]
        [CDisplayName("Xã/Phường")]
        public string? Ward { get; set; }

        [Required]
        [CDisplayName("Số nhà")]
        public string? Detail { get; set; }

        public Guid AccountId { get; set; }
    }
}
