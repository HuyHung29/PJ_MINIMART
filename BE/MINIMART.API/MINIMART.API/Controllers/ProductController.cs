using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MINIMART.BL.IServices;
using MINIMART.Common.Entities.DTO;
using MINIMART.Common.Entities.Models;
using System.Security.Claims;

namespace MINIMART.API.Controllers
{
    [Route("api/v1/[controller]")]
    /*[Authorize(Roles = "Admin")]*/
    [AllowAnonymous]
    public class ProductController : BaseController<Product>
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService) : base(productService)
        {
            _productService = productService;
        }

        [HttpPost("filter")]
        public override async Task<IActionResult> GetByFilterAndPaging(PagingObject filter)
        {
            var result = await _productService.GetByFilterAndPaging(filter);

            return Ok(result);

        }

        [HttpPost("add")]
        public async Task<IActionResult> AddProduct([FromForm] ProductDTO product)
        {
            try
            {
                var userName = User.FindFirstValue(ClaimTypes.Name);
                var result = await _productService.Insert(product, "Huy Hung");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return base.HandleException(ex);
            }
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromForm] ProductDTO entity)
        {
            try
            {
                var userName = User.FindFirstValue(ClaimTypes.Name);
                var result = await _productService.Update(id, entity, "Huy Hung");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return base.HandleException(ex);
            }
        }

    }
}
