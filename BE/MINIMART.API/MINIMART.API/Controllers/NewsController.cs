using MINIMART.BL.IServices;
using MINIMART.Common.Entities.Models;

namespace MINIMART.API.Controllers
{
    public class NewsController : BaseController<News>
    {
        public NewsController(IBaseService<News> baseService) : base(baseService)
        {
        }
    }
}
