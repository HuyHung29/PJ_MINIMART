using MINIMART.Common.Entities.DTO;
using MINIMART.Common.Entities.Models;

namespace MINIMART.BL.IServices
{
    public interface ICartService : IBaseService<Cart>
    {
        public Task<PagingResult<Product>> GetByFilter(Guid accId, PagingObject filter);
        public Task<ServiceResponse<Cart>> Insert(Guid id, CartDTO item);

        public Task<ServiceResponse<Cart>> Update(Guid id, CartDTO item);

        public Task<ServiceResponse<List<Guid>>> Delete(Guid accId, List<Guid> proIds);
    }
}
