using MINIMART.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace MINIMART.Common.Entities.Models
{
    public class Product : Base
    {
        [Key]
        public Guid ProductId { get; set; }

        [Required]
        public string? ProductCode { get; set; }

        [Required]
        public string? ProductName { get; set; }

        public int Quantity { get; set; }

        public Unit Unit { get; set; }

        public string? UnitName
        {
            get
            {
                return Unit switch
                {
                    Unit.Kg => "Kg",
                    Unit.Gam => "Gam",
                    _ => null,
                };
            }

        }

        public int Weight { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public decimal CurrentPrice
        {
            get
            {
                return Price - Price * Discount;
            }
        }

        public List<Picture>? Pictures { get; set; }

        [Required]
        public Guid SupplierId { get; set; }

        public string? SupplierName { get; set; }

        public string? CategoryName { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
    }
}
