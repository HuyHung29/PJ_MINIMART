using MINIMART.BL.IServices;
using MINIMART.Common.Entities.Models;

namespace MINIMART.API.Controllers
{
    public class UserController : BaseController<User>
    {
        public UserController(IBaseService<User> baseService) : base(baseService)
        {
        }
    }
}
