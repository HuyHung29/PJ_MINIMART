using MINIMART.Common.Enums;

namespace MINIMART.Common.Entities.Models
{
    public class OrderP : Base
    {
        public Guid OrderId { get; set; }

        public string? OrderCode { get; set; }

        public Guid? AccountId { get; set; }

        public decimal? Total { get; set; }

        public OrderStatus? Status { get; set; }

        public string? StatusName
        {
            get
            {
                return Status switch
                {
                    OrderStatus.Waiting => "Chờ xác nhận",
                    OrderStatus.Delivered => "Đang vận chuyển",
                    OrderStatus.Cancled => "Đã hủy",
                    OrderStatus.Done => "Đã giao",
                    _ => "",
                };
            }
        }

        public string? DeliveryPlace { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public List<ProductP>? Products { get; set; }

        public string? Receiver { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Note { get; set; }
    }
}
