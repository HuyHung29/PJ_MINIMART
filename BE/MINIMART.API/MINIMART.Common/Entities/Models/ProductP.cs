using MINIMART.Common.Enums;

namespace MINIMART.Common.Entities.Models
{
    public class ProductP
    {
        public Guid ProductId { get; set; }

        public string? ProductCode { get; set; }

        public string? ProductName { get; set; }

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

        public string? Url { get; set; }

        public Guid SupplierId { get; set; }

        public string? SupplierName { get; set; }

        public string? CategoryName { get; set; }

        public Guid CategoryId { get; set; }
    }
}
