﻿using MINIMART.Common.Enums;
using System.ComponentModel.DataAnnotations;
using static MINIMART.Common.Entities.Attributes.CustomAtrribute;

namespace MINIMART.Common.Entities.Models
{
    public class User
    {
        public Guid AccountId { get; set; }

        public string? UserCode { get; set; }

        [Required]
        [CDisplayName("Họ tên")]
        public string FullName { get; set; }

        public string? UserName { get; set; }

        public string? Avatar { get; set; }

        public Gender Gender { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime? DOB { get; set; }

        public string? Address { get; set; }

        public Role Role { get; set; }
    }
}
