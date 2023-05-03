using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MINIMART.BL.IServices;
using MINIMART.Common.Entities.DTO;
using MINIMART.Common.Entities.Models;
using System.Security.Claims;

namespace MINIMART.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : BaseController<Category>
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService) : base(categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCate([FromForm] CategoryDTO entity)
        {
            try
            {
                var userName = User.FindFirstValue(ClaimTypes.Name);
                var result = await _categoryService.Insert(entity, userName);
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
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] CategoryDTO entity)
        {
            try
            {
                var userName = User.FindFirstValue(ClaimTypes.Name);
                var result = await _categoryService.Update(id, entity, userName);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return base.HandleException(ex);
            }
        }
    }
}
