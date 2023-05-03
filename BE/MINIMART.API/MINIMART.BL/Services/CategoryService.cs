using MINIMART.BL.IServices;
using MINIMART.Common.Entities.DTO;
using MINIMART.Common.Entities.Models;
using MINIMART.Common.Exceptions;
using MINIMART.Common.Resources;
using MINIMART.DL.IRepository;

namespace MINIMART.BL.Services
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        private readonly IPhotoService _photoService;
        public CategoryService(IBaseDL<Category> baseDL, IPhotoService photoService) : base(baseDL)
        {
            _photoService = photoService;
        }

        public async Task<ServiceResponse<Category>> Insert(CategoryDTO cate, string userName)
        {
            var res = new ServiceResponse<Category>();

            var validateResult = await base.HandleValidate(cate);

            if (validateResult.IsValid)
            {
                if (cate.Thumbnail != null)
                {
                    var thumbnail = await _photoService.AddPhotoAsync(cate.Thumbnail);

                    if (thumbnail != null)
                    {
                        var category = new Category
                        {
                            CategoryId = cate.CategoryId,
                            CategoryName = cate.CategoryName,
                            Thumbnail = thumbnail.Url.ToString()
                        };

                        var insertedRecord = await _baseDL.Insert(category, userName);

                        if (insertedRecord != null)
                        {
                            res.Success = true;
                            res.Data = insertedRecord;
                            res.Message = string.Format(Resource.InsertSuccess, "danh mục");
                        }
                        else
                        {
                            res.Success = false;
                            res.Message = string.Format(Resource.InsertError, "danh mục");
                            res.Error = new ErrorResult
                            {
                                DevMes = string.Format(Resource.NotExist, "danh mục"),
                                UserMes = Resource.HelpText
                            };
                        }
                    }
                    else
                    {
                        res.Success = false;
                        res.Message = string.Format(Resource.InsertError, "danh mục");
                        res.Error = new ErrorResult
                        {
                            DevMes = Resource.UploadError,
                            UserMes = Resource.HelpText
                        };
                    }
                }
                else
                {
                    res.Success = false;
                    res.Message = "Ảnh mô tả không được để trống";
                    res.Error = new ErrorResult
                    {
                        DevMes = Resource.UploadError,
                        UserMes = Resource.HelpText,
                    };
                }

            }
            else
            {
                throw new ValidateException(Resource.ValidateError, validateResult.Errors);
            }

            return res;
        }

        protected override async Task<ValidateResult> CustomValidate(ValidateResult validateResult, Category entity)
        {
            if (entity.CategoryId != Guid.Empty)
            {
                var cateId = await _baseDL.GetById(entity.CategoryId);

                if (cateId == null)
                {
                    validateResult.IsValid = false;
                    validateResult.Errors.Add("CategoryId", "Không tồn tại danh mục");
                }
            }

            var categoryName = await _baseDL.GetByName(entity.CategoryName, entity.CategoryId);

            if (categoryName)
            {
                validateResult.IsValid = false;
                validateResult.Errors.Add("CategoryName", "Tên danh mục đã tồn tại");
            }

            return validateResult;
        }

        public async Task<ServiceResponse<Category>> Update(Guid id, CategoryDTO cate, string userName)
        {
            var res = new ServiceResponse<Category>();

            var validateResult = await base.HandleValidate(cate);

            if (validateResult.IsValid)
            {
                Category category = new Category();
                if (cate.Thumbnail != null)
                {
                    var thumbnail = await _photoService.AddPhotoAsync(cate.Thumbnail);
                    if (thumbnail != null)
                    {
                        category = new Category
                        {
                            CategoryId = cate.CategoryId,
                            CategoryName = cate.CategoryName,
                            Thumbnail = thumbnail.Url.ToString()
                        };
                    }
                    else
                    {
                        res.Success = false;
                        res.Message = string.Format(Resource.InsertError, "danh mục");
                        res.Error = new ErrorResult
                        {
                            DevMes = string.Format(Resource.NotExist, "danh mục"),
                            UserMes = Resource.HelpText
                        };
                    }
                }
                else
                {
                    category = new Category
                    {
                        CategoryId = cate.CategoryId,
                        CategoryName = cate.CategoryName,
                        Thumbnail = cate.OldThumbnail
                    };
                }
                var isUpdated = await _baseDL.Update(id, category, userName);

                if (isUpdated != null)
                {
                    res.Success = true;
                    res.Data = category;
                    res.Message = string.Format(Resource.UpdateSuccess, "danh mục");
                }
                else
                {
                    res.Success = false;
                    res.Message = string.Format(Resource.UpdateError, "danh mục");
                    res.Error = new ErrorResult
                    {
                        DevMes = string.Format(Resource.NotExist, "danh mục"),
                        UserMes = Resource.HelpText
                    };
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
