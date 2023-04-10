using MINIMART.BL.IServices;
using MINIMART.Common.Entities.DTO;
using MINIMART.Common.Entities.Models;
using MINIMART.DL.IRepository;

namespace MINIMART.BL.Services
{
    public class ProductService : BaseService<Product>, IProductService
    {
        private readonly IProductDL _productDL;

        public ProductService(IProductDL productDL) : base(productDL)
        {
            _productDL = productDL;
        }

        public new async Task<PagingResult<Product>> GetByFilterAndPaging(PagingObject filter)
        {
            var result = await _productDL.GetByFilterAndPaging(filter);

            return result;
        }
    }
}
