using MINIMART.BL.IServices;
using MINIMART.Common.Entities.Models;

namespace MINIMART.API.Controllers
{
    public class SupplierController : BaseController<Supplier>
    {
        public SupplierController(IBaseService<Supplier> baseService) : base(baseService)
        {
        }
    }
}
