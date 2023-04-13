using MINIMART.Common.Entities.Models;

namespace MINIMART.Common.Entities.DTO
{
    public class LoginResult
    {
        public User User { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
