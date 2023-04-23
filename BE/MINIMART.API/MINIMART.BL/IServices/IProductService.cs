using MINIMART.Common.Entities.DTO;
using MINIMART.Common.Entities.Models;

namespace MINIMART.BL.IServices
{
    public interface IProductService : IBaseService<Product>
    {
        public Task<ServiceResponse<Product>> Insert(ProductDTO product, string userName);

        public Task<ServiceResponse<Product>> Update(Guid id, ProductDTO product, string userName);

    }
}
