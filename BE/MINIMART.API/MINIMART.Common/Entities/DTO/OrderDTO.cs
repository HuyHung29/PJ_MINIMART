using MINIMART.Common.Entities.Models;
using MINIMART.Common.Enums;

namespace MINIMART.Common.Entities.DTO
{
    public class OrderDTO
    {
        public Guid OrderId { get; set; }

        public Guid AccountId { get; set; }

        public OrderStatus Status { get; set; }

        public List<ProductP>? Products { get; set; }
    }
}
