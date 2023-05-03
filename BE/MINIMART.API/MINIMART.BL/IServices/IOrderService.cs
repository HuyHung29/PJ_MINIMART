using MINIMART.Common.Entities.DTO;
using MINIMART.Common.Entities.Models;

namespace MINIMART.BL.IServices
{
    public interface IOrderService
    {
        public Task<PagingResult<OrderP>> GetFilterAndPaging(PagingObject filter);

        public Task<ServiceResponse<bool>> Insert(Order order);

        public Task<ServiceResponse<bool>> ChangeStatus(OrderDTO order);
    }
}
