using MINIMART.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace MINIMART.Common.Entities.Models
{
    public class User
    {
        [Key]
        public Guid AccountId { get; set; }

        [Required]
        public string UserCode { get; set; }

        [Required]
        public string FullName { get; set; }

        public string? Avatar { get; set; }

        public Gender Gender { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime DOB { get; set; }

        public List<Address>? Address { get; set; }
    }
}
