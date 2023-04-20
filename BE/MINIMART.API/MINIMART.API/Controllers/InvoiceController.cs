using Microsoft.AspNetCore.Mvc;
using MINIMART.BL.IServices;
using MINIMART.Common.Entities.Models;

namespace MINIMART.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : BaseController<Invoice>
    {
        public InvoiceController(IBaseService<Invoice> baseService) : base(baseService)
        {
        }
    }
}
