using Microsoft.AspNetCore.Mvc;
using MINIMART.BL.IServices;
using MINIMART.Common.Entities.Models;

namespace MINIMART.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : BaseController<Cart>
    {
        public CartController(IBaseService<Cart> baseService) : base(baseService)
        {
        }
    }
}
