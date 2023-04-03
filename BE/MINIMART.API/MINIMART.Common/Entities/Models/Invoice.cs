using System.ComponentModel.DataAnnotations;

namespace MINIMART.Common.Entities.Models
{
    public class Invoice : Base
    {
        [Key]
        public Guid InvoiceId { get; set; }

        [Required]
        public string? InvoiceCode { get; set; }

        public string? Title { get; set; }

        [Required]
        public Guid SupplierId { get; set; }

        [Required]
        public List<Product>? Products { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
