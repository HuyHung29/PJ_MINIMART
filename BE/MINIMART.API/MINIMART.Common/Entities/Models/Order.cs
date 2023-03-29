using MINIMART.Common.Enums;

namespace MINIMART.Common.Entities.Models
{
    public class Order : Base
    {
        public Guid OrderId { get; set; }

        public string OrderCode { get; set; }

        public Guid AccountId { get; set; }

        public decimal Total { get; set; }

        public OrderStatus Status { get; set; }

        public string StatusName
        {
            get
            {
                return Status switch
                {
                    OrderStatus.Approved => "Đã tiếp nhận",
                    OrderStatus.Delivered => "Đang vận chuyển",
                    OrderStatus.Failed => "Thất bại",
                    OrderStatus.Cancled => "Đã hủy",
                    OrderStatus.Done => "Thành công",
                    _ => "",
                };
            }
        }

        public string DeliveryPlace { get; set; }

        public DateTime DeliveryDate { get; set; }

        public Product Products { get; set; }
    }
}
