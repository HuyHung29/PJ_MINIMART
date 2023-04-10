using Microsoft.AspNetCore.Mvc;
using MINIMART.BL.IServices;
using MINIMART.Common.Entities.DTO;
using MINIMART.Common.Entities.Models;

namespace MINIMART.API.Controllers
{
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
    }
}
