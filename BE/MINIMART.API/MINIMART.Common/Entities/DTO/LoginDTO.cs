using System.ComponentModel.DataAnnotations;
using static MINIMART.Common.Entities.Attributes.CustomAtrribute;

namespace MINIMART.Common.Entities.DTO
{
    public class LoginDTO
    {
        [Required]
        [CDisplayName("Tên đăng nhập")]
        public string UserName { get; set; }

        [Required]
        [CDisplayName("Mật khẩu")]
        public string Password { get; set; }
    }
}
