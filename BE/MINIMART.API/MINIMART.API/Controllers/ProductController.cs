using Microsoft.AspNetCore.Mvc;
using MINIMART.BL.IServices;
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

        [HttpGet]
        public override async Task<IActionResult> GetByFilterAndPaging()
        {
            var result = await _productService.GetByFilterAndPaging();

            return Ok(result);

        }
    }
}
