using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MINIMART.Common.Entities.DTO
{
    public class AuthDTO
    {
        [Required]
        [DisplayName("Tên đăng nhập")]
        public string UserName { get; set; }

        [Required]
        [DisplayName("Mật khẩu")]
        public string Password { get; set; }
    }
}
