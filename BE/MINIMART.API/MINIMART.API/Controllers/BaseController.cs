using Microsoft.AspNetCore.Mvc;
using MINIMART.BL.IServices;

namespace MINIMART.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        private readonly IBaseService<T> _baseService;

        public BaseController(IBaseService<T> baseService)
        {
            _baseService = baseService;
        }


        [HttpGet]
        public virtual async Task<IActionResult> GetByFilterAndPaging()
        {
            var result = await _baseService.GetByFilterAndPaging();

            return Ok(result);

        }
    }
}
