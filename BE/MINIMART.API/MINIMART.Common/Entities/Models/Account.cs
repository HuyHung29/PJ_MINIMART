using MINIMART.Common.Enums;

namespace MINIMART.Common.Entities.Models
{
    public class Account
    {
        public Guid AccountId { get; set; }

        public string UserName { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public Role Role { get; set; }

        public AccountStatus IsActive { get; set; }
    }
}
