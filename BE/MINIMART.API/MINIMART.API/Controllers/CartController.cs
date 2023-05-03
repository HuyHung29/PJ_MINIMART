using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MINIMART.BL.IServices;
using MINIMART.Common.Entities.DTO;
using MINIMART.Common.Entities.Models;
using System.Security.Claims;

namespace MINIMART.API.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CartController : BaseController<Cart>
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService) : base(cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("get-all")]
        public async Task<IActionResult> GetByFilter(PagingObject filter)
        {
            try
            {
                var acc = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = await _cartService.GetByFilter(Guid.Parse(acc), filter);

                return Ok(result);
            }
            catch (Exception ex)
            {

                return base.HandleException(ex);
            }

        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(CartDTO cart)
        {
            try
            {
                var acc = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var res = await _cartService.Insert(Guid.Parse(acc), cart);

                if (res.Success)
                {
                    return Ok(res);
                }
                else
                {
                    return BadRequest(res);
                }
            }
            catch (Exception ex)
            {
                return base.HandleException(ex);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCart(CartDTO cart)
        {
            try
            {
                var acc = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var res = await _cartService.Update(Guid.Parse(acc), cart);

                if (res.Success)
                {
                    return Ok(res);
                }
                else
                {
                    return BadRequest(res);
                }
            }
            catch (Exception ex)
            {
                return base.HandleException(ex);
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteCart([FromBody] List<Guid> ids)
        {

            try
            {
                var acc = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = await _cartService.Delete(Guid.Parse(acc), ids);

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
