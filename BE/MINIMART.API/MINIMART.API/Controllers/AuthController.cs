using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MINIMART.BL.IServices;
using MINIMART.Common.Entities.DTO;
using MINIMART.Common.Exceptions;
using MINIMART.Common.Resources;

namespace MINIMART.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;


        public AuthController(IAuthService authService)
        {
            _authService = authService;
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

        [HttpPost("register")]
        public async Task<IActionResult> Register(AuthDTO auth)
        {
            try
            {
                var result = await _authService.Register(auth);

                if (result.Success)
                {
                    // Todo: Gửi mail xác thực tài khoản

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

        [HttpGet("active")]
        public async Task<IActionResult> ActiveAccount([FromQuery] Guid accId)
        {
            try
            {
                var result = await _authService.Active(accId);

                if (result)
                {
                    RedirectResult redirect = new("https://www.youtube.com/", true);
                    return redirect;
                }
                else
                {
                    RedirectResult redirect = new("https://learn.microsoft.com/", true);
                    return redirect;
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO acc)
        {
            try
            {
                var result = await _authService.Login(acc);

                if (result.Success)
                {
                    return Ok(result);
                }

                return Unauthorized(result);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}
