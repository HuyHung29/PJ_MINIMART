namespace MINIMART.Common.Entities.Models
{
    public class Coupon
    {
        public Guid CouponId { get; set; }

        public string CouponCode { get; set; }

        public DateTime ApplyDate { get; set; }

        public DateTime ExpireDate { get; set; }

        public decimal MinPrice { get; set; }

        public decimal MaxPrice { get; set; }

        public decimal Discount { get; set; }
    }
}
