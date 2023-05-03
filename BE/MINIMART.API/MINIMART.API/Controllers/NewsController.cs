using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MINIMART.BL.IServices;
using MINIMART.Common.Entities.DTO;
using MINIMART.Common.Entities.Models;
using System.Security.Claims;

namespace MINIMART.API.Controllers
{
    [AllowAnonymous]
    public class NewsController : BaseController<News>
    {
        private readonly INewsService _newsService;
        public NewsController(INewsService newsService) : base(newsService)
        {
            _newsService = newsService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCate([FromForm] NewsDTO entity)
        {
            try
            {
                var userName = User.FindFirstValue(ClaimTypes.Name);
                var result = await _newsService.Insert(entity, userName);
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

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] NewsDTO entity)
        {
            try
            {
                var userName = User.FindFirstValue(ClaimTypes.Name);
                var result = await _newsService.Update(id, entity, userName);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return base.HandleException(ex);
            }
        }
    }
}
