using MINIMART.Common.Enums;
using System.ComponentModel.DataAnnotations;
using static MINIMART.Common.Entities.Attributes.CustomAtrribute;

namespace MINIMART.Common.Entities.DTO
{
    public class UserDTO
    {
        [Required]
        [CDisplayName("Họ tên")]
        public string FullName { get; set; }

        public string? Avatar { get; set; }

        public Gender Gender { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime? DOB { get; set; }

        [Required]
        [CDisplayName("Địa chỉ")]
        public string Address { get; set; }

    }
}
