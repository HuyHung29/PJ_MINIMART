using MINIMART.Common.Entities.DTO;
using MINIMART.Common.Entities.Models;

namespace MINIMART.DL.IRepository
{
    public interface ICartDL : IBaseDL<Cart>
    {
        public Task<PagingResult<Product>> GetByFilter(Guid accId, PagingObject filter);
        public Task<bool> InsertCart(CartDTO cartDTO);

        public Task<bool> UpdateCart(CartDTO cartDTO);

        public Task<bool> DeleteCart(Guid accId, string ids);

        public Task<bool> CheckCart(CartDTO item);

    }
}
