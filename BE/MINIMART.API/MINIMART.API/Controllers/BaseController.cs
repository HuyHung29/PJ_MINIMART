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
    [AllowAnonymous]
    public class BaseController<T> : ControllerBase
    {
        protected readonly IBaseService<T> _baseService;

        public BaseController(IBaseService<T> baseService)
        {
            _baseService = baseService;
        }

        [NonAction]
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

        [AllowAnonymous]
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] T entity)
        {
            try
            {
                var userName = User.FindFirstValue(ClaimTypes.Name);
                var result = await _baseService.Insert(entity, userName);

                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] T entity)
        {
            try
            {
                var userName = User.FindFirstValue(ClaimTypes.Name);
                var result = await _baseService.Update(id, entity, userName);
                if (result.Success)
                {
                    return Ok(result);

                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] List<Guid> ids)
        {
            try
            {
                var result = await _baseService.Delete(ids);

                if (result.Success)
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, result);
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}
