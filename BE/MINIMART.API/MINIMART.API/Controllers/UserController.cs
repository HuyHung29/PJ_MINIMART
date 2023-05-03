using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MINIMART.BL.IServices;
using MINIMART.Common.Entities.Models;
using System.Security.Claims;

namespace MINIMART.API.Controllers
{
    [Authorize]
    public class UserController : BaseController<User>
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) : base(userService)
        {
            _userService = userService;
        }

        [HttpPut("edit")]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            try
            {
                var accId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = await _userService.Update(Guid.Parse(accId), user, "");

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
                return base.HandleException(ex);
            }
        }
    }
}
