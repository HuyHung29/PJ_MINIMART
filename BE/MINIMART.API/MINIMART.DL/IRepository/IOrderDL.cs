using MINIMART.Common.Entities.DTO;
using MINIMART.Common.Entities.Models;

namespace MINIMART.DL.IRepository
{
    public interface IOrderDL
    {
        public Task<PagingResult<OrderP>> GetFilterAndPaging(PagingObject filter);

        public Task<bool> Insert(Order order, string ids, string where);

        public Task<bool> ChangeStatus(OrderDTO order);

        public Task<bool> CancelOrder(OrderDTO order, string ids, string where);


        public Task<List<Product>> GetProductById(string ids);

        public Task<Order> GetOrderById(Guid id);
    }
}
