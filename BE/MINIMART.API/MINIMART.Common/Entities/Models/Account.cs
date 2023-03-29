using MINIMART.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace MINIMART.Common.Entities.Models
{
    public class Account
    {
        [Key]
        public Guid AccountId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public Role Role { get; set; }
    }
}
