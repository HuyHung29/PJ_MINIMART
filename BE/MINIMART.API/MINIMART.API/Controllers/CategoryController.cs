using MINIMART.BL.IServices;
using MINIMART.Common.Entities.Models;

namespace MINIMART.API.Controllers
{
    public class CategoryController : BaseController<Category>
    {
        public CategoryController(IBaseService<Category> baseService) : base(baseService)
        {
        }
    }
}
