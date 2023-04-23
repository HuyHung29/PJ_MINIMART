using MINIMART.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace MINIMART.Common.Entities.Models
{
    public class Product : Base
    {
        private Product product;

        [Key]
        public Guid ProductId { get; set; }

        [Required]
        public string? ProductCode { get; set; }

        [Required]
        public string? ProductName { get; set; }

        [Required]
        public int Quantity { get; set; }

        public UnitEnum Unit { get; set; }

        public string? UnitName
        {
            get
            {
                return Unit switch
                {
                    UnitEnum.Box => "Hộp",
                    UnitEnum.Pack => "Túi",
                    UnitEnum.Bottle => "Chai",
                    _ => "",
                };
            }

        }

        public int? Weight { get; set; }

        public VolumeEnum? Volume { get; set; }

        public string? VolumeName
        {
            get
            {
                return Volume switch
                {
                    VolumeEnum.Gam => "Gam",
                    VolumeEnum.Kg => "Kg",
                    VolumeEnum.Lit => "L",
                    VolumeEnum.MlLit => "mL",
                    _ => "",
                };
            }
        }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public decimal? Discount { get; set; }

        public decimal? CurrentPrice
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
