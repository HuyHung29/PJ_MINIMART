using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MINIMART.BL.IServices;
using MINIMART.Common.Entities.DTO;
using MINIMART.Common.Entities.Models;

namespace MINIMART.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : BaseController<Product>
    {
        private readonly IProductService _productService;
        private readonly IPhotoService _photoService;

        public ProductController(IProductService productService, IPhotoService photoService) : base(productService)
        {
            _productService = productService;
            _photoService = photoService;
        }

        [HttpPost("filter")]
        public override async Task<IActionResult> GetByFilterAndPaging(PagingObject filter)
        {
            var result = await _productService.GetByFilterAndPaging(filter);

            return Ok(result);

        }

        [AllowAnonymous]
        [HttpPost("upload")]
        public async Task<IActionResult> UploadPhoto(IFormFileCollection files)
        {
            var result = await _photoService.AddMultiplePhotoAsync(files);
            return Ok(result);
        }
    }
}
