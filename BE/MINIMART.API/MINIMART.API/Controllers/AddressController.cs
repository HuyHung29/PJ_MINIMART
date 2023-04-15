using MINIMART.BL.IServices;
using MINIMART.Common.Entities.Models;

namespace MINIMART.API.Controllers
{
    public class AddressController : BaseController<Address>
    {
        public AddressController(IBaseService<Address> baseService) : base(baseService)
        {
        }
    }
}
