using Microsoft.AspNetCore.Mvc;
using MINIMART.BL.IServices;
using MINIMART.Common.Entities.DTO;

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


        [HttpPost("filter")]
        public virtual async Task<IActionResult> GetByFilterAndPaging(PagingObject filter)
        {
            try
            {
                var result = await _baseService.GetByFilterAndPaging(filter);

                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] T entity)
        {
            try
            {
                var result = await _baseService.Insert(entity);

                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
