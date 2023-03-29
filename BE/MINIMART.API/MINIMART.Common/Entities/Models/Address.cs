using System.ComponentModel.DataAnnotations;

namespace MINIMART.Common.Entities.Models
{
    public class Address
    {
        [Key]
        public Guid AddressId { get; set; }

        public string? City { get; set; }

        public string? District { get; set; }

        public string? Ward { get; set; }

        public string? Detail { get; set; }

        public Guid AccountId { get; set; }
    }
}
