using System.ComponentModel.DataAnnotations;
using static MINIMART.Common.Entities.Attributes.CustomAtrribute;

namespace MINIMART.Common.Entities.Models
{
    public class Supplier
    {
        [Key]
        public Guid SupplierId { get; set; }

        [Required]
        [CDisplayName("Mã nhà cung cấp")]
        public string? SupplierCode { get; set; }

        [Required]
        [CDisplayName("Tên nhà cung cấp")]
        public string? SupplierName { get; set; }

        [Required]
        [CDisplayName("Địa chỉ nhà cung cấp")]
        public string Address { get; set; }
    }
}
