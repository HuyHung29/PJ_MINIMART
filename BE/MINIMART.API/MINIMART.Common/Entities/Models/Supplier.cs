using System.ComponentModel.DataAnnotations;

namespace MINIMART.Common.Entities.Models
{
    public class Supplier
    {
        [Key]
        public Guid SupplierId { get; set; }

        [Required]
        public string? SupplierCode { get; set; }

        [Required]
        public string? SupplierName { get; set; }
    }
}
