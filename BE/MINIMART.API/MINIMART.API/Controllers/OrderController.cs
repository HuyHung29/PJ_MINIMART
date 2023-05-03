using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MINIMART.BL.IServices;
using MINIMART.Common.Entities.DTO;
using MINIMART.Common.Entities.Models;
using MINIMART.Common.Exceptions;
using MINIMART.Common.Resources;
using System.Security.Claims;

namespace MINIMART.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
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

        [HttpPost("filter")]
        public async Task<IActionResult> GetFilterAngPaging(PagingObject filter)
        {
            try
            {
                var result = await _orderService.GetFilterAndPaging(filter);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPost("get-by-account")]
        public async Task<IActionResult> GetFilterAngPagingAcc(PagingObject filter)
        {
            try
            {
                var acc = User.FindFirstValue(ClaimTypes.NameIdentifier);
                filter.AccountId = Guid.Parse(acc);
                var result = await _orderService.GetFilterAndPaging(filter);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Insert(Order order)
        {
            try
            {
                var acc = User.FindFirstValue(ClaimTypes.NameIdentifier);
                order.AccountId = Guid.Parse(acc);

                var result = await _orderService.Insert(order);

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

        [HttpPut("change-status")]
        public async Task<IActionResult> ChangeStatus(OrderDTO order)
        {
            try
            {
                var result = await _orderService.ChangeStatus(order);

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
    }
}
