using MINIMART.Common.Entities.DTO;
using MINIMART.Common.Entities.Models;

namespace MINIMART.DL.IRepository
{
    public interface IProductDL : IBaseDL<Product>
    {
        public Task<Product> Insert(ProductDTO product, string userName);

        public Task<bool> InsertPicture(List<Picture> pictures);

        public Task<Product> UpdateProduct(ProductDTO product, Guid id, string userName);

        public Task<bool> DeletePicture(Guid id);
    }
}
