using Microsoft.AspNetCore.Http;
using MINIMART.BL.IServices;
using MINIMART.Common.Entities.DTO;
using MINIMART.Common.Entities.Models;
using MINIMART.Common.Exceptions;
using MINIMART.Common.Resources;
using MINIMART.DL.IRepository;

namespace MINIMART.BL.Services
{
    public class ProductService : BaseService<Product>, IProductService
    {
        private readonly IProductDL _productDL;
        private readonly IPhotoService _photoService;

        public ProductService(IProductDL productDL, IPhotoService photoService) : base(productDL)
        {
            _productDL = productDL;
            _photoService = photoService;
        }

        public new async Task<PagingResult<Product>> GetByFilterAndPaging(PagingObject filter)
        {
            var result = await _productDL.GetByFilterAndPaging(filter);

            return result;
        }

        public async Task<ServiceResponse<Product>> Insert(ProductDTO product, string userName)
        {
            var res = new ServiceResponse<Product>();

            var validateResult = await base.HandleValidate(product);

            if (validateResult.IsValid)
            {
                if (product.Pictures != null)
                {
                    var productInsert = await _productDL.Insert(product, userName);
                    if (productInsert != null)
                    {
                        var pictures = await HandleUploadPicture(product.Pictures, productInsert.ProductId);

                        if (pictures != null)
                        {
                            productInsert.Pictures = pictures;

                            var checkInsert = await _productDL.InsertPicture(pictures);

                            if (checkInsert)
                            {
                                res.Success = true;
                                res.Message = string.Format(Resource.InsertSuccess, "sản phẩm");
                                res.Data = productInsert;
                            }
                            else
                            {
                                res.Success = false;
                                res.Message = Resource.HelpText;
                            }

                        }
                        else
                        {
                            res.Success = false;
                            res.Message = Resource.HelpText;

                            await _productDL.Delete(productInsert.ProductId.ToString());
                        }
                    }
                    else
                    {
                        res.Success = false;
                        res.Message = Resource.HelpText;
                    }
                }
                else
                {
                    res.Success = false;
                    res.Message = Resource.HelpText;
                }
            }
            else
            {
                throw new ValidateException(Resource.ValidateError, validateResult.Errors);
            }

            return res;
        }

        private async Task<List<Picture>> HandleUploadPicture(IFormFileCollection pictures, Guid id)
        {
            List<Picture> result = new List<Picture>();

            var uploadResult = await _photoService.AddMultiplePhotoAsync(pictures);

            foreach (var item in uploadResult)
            {
                var picture = new Picture
                {
                    PictureId = item.PublicId,
                    ProductId = id,
                    Url = item.Url.ToString()
                };

                result.Add(picture);
            }

            return result;
        }

        protected override async Task<ValidateResult> CustomValidate(ValidateResult validateResult, Product entity)
        {
            var checkName = await _productDL.GetByName(entity.ProductName, entity.ProductId);
            var checkCode = await _productDL.GetByCode(entity.ProductCode, entity.ProductId);

            if (checkCode)
            {
                validateResult.IsValid = false;
                validateResult?.Errors?.Add("ProductCode", "Mã sản phẩm đã tồn tại");
            }

            if (checkName)
            {
                validateResult.IsValid = false;
                validateResult?.Errors?.Add("ProductName", "Tên sản phẩm đã tồn tại");
            }

            return validateResult;
        }

        public async Task<ServiceResponse<Product>> Update(Guid id, ProductDTO product, string userName)
        {
            var res = new ServiceResponse<Product>();

            var validateResult = await base.HandleValidate(product);

            if (validateResult.IsValid)
            {
                if (product.Pictures != null)
                {
                    var productUpdate = await _productDL.UpdateProduct(product, id, userName);
                    if (productUpdate != null)
                    {
                        var pictures = await HandleUploadPicture(product.Pictures, id);

                        if (pictures != null)
                        {
                            var checkDelete = await _productDL.DeletePicture(id);
                            if (checkDelete)
                            {
                                var checkInsert = await _productDL.InsertPicture(pictures);

                                if (checkInsert)
                                {
                                    res.Success = true;
                                    res.Message = string.Format(Resource.UpdateSuccess, "sản phẩm");
                                    productUpdate.Pictures = pictures;
                                    res.Data = productUpdate;
                                }
                                else
                                {
                                    res.Success = false;
                                    res.Message = Resource.HelpText;
                                }
                            }
                            else
                            {
                                res.Success = false;
                                res.Message = Resource.HelpText;
                            }
                        }
                        else
                        {
                            res.Success = false;
                            res.Message = Resource.HelpText;
                        }
                    }
                    else
                    {
                        res.Success = false;
                        res.Message = Resource.HelpText;
                    }
                }
                else
                {
                    var productUpdate = await _productDL.UpdateProduct(product, id, userName);
                    if (productUpdate != null)
                    {
                        res.Success = true;
                        res.Message = string.Format(Resource.UpdateSuccess, "sản phẩm");
                        res.Data = productUpdate;
                    }
                    else
                    {
                        res.Success = false;
                        res.Message = Resource.HelpText;
                    }
                }
            }
            else
            {
                throw new ValidateException(Resource.ValidateError, validateResult.Errors);
            }

            return res;
        }
    }
}
