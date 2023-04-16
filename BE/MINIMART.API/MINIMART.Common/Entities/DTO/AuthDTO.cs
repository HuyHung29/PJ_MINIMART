using System.ComponentModel.DataAnnotations;
using static MINIMART.Common.Entities.Attributes.CustomAtrribute;

namespace MINIMART.Common.Entities.DTO
{
    public class AuthDTO
    {
        [Required]
        [CDisplayName("Tên đăng nhập")]
        public string UserName { get; set; }

        [Required]
        [CDisplayName("Mật khẩu")]
        public string Password { get; set; }

        [CDisplayName("Số điện thoại")]
        public string? PhoneNumber { get; set; }

        [Required]
        [CDisplayName("Họ tên")]
        public string FullName { get; set; }

        public byte[]? PasswordHash { get; set; } = null;

        public byte[]? PasswordSalt { get; set; } = null;
    }
}
