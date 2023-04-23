using MINIMART.Common.Entities.DTO;
using MINIMART.Common.Entities.Models;

namespace MINIMART.BL.IServices
{
    public interface ICategoryService : IBaseService<Category>
    {

        public Task<ServiceResponse<Category>> Insert(CategoryDTO cate, string userName);
        public Task<ServiceResponse<Category>> Update(Guid id, CategoryDTO cate, string userName);
    }
}
