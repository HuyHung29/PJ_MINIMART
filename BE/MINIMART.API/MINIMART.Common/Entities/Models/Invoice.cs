using System.ComponentModel.DataAnnotations;

namespace MINIMART.Common.Entities.Models
{
    public class Invoice : Base
    {
        [Key]
        public Guid InvoiceId { get; set; }

        [Required]
        public string? InvoiceCode { get; set; }

        [Required]  
        public Guid SupplierId { get; set; }

        public Product? Products { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
