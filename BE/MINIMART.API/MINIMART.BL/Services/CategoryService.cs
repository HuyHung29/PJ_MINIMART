using MINIMART.BL.IServices;
using MINIMART.Common.Entities.Models;
using MINIMART.DL.IRepository;

namespace MINIMART.BL.Services
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        public CategoryService(IBaseDL<Category> baseDL) : base(baseDL)
        {
        }
    }
}
