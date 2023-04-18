using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MINIMART.BL.IServices;
using MINIMART.Common.Entities.DTO;
using MINIMART.Common.Exceptions;
using MINIMART.Common.Resources;
using System.Security.Claims;

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

        protected IActionResult HandleException(Exception exception)
        {
            Console.WriteLine(exception);
            Console.WriteLine(exception.GetType());

            if (exception is ValidateException)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                {
                    DevMes = Resource.Dev_ValidateError,
                    UserMes = exception.Message,
                    MoreInfo = exception.Data
                });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    DevMes = exception.Message,
                    UserMes = Resource.HelpText,
                });
            }
        }

        [HttpPost("filter")]
        public virtual async Task<IActionResult> GetByFilterAndPaging(PagingObject filter)
        {
            try
            {
                var result = await _baseService.GetByFilterAndPaging(filter);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] T entity)
        {
            try
            {
                var result = await _baseService.Insert(entity);

                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] T entity)
        {
            try
            {

                return Ok();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] List<Guid> ids)
        {
            try
            {
                var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return Ok(id);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}
