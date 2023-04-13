using Microsoft.AspNetCore.Authorization;
using MINIMART.BL.IServices;
using MINIMART.Common.Entities.Models;

namespace MINIMART.API.Controllers
{
    [Authorize]
    public class UserController : BaseController<User>
    {
        public UserController(IBaseService<User> baseService) : base(baseService)
        {
        }
    }
}
