
using MINIMART.BL.IServices;
using MINIMART.Common.Entities.DTO;
using MINIMART.Common.Exceptions;
using MINIMART.Common.Resources;
using MINIMART.DL.IRepository;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using static MINIMART.Common.Entities.Attributes.CustomAtrribute;

namespace MINIMART.BL.Services
{
    public class BaseService<T> : IBaseService<T>
    {
        protected IBaseDL<T> _baseDL;

        public BaseService(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }

        public async Task<PagingResult<T>> GetByFilterAndPaging(PagingObject filter)
        {
            var result = await _baseDL.GetByFilterAndPaging(filter);

            return result;
        }

        public async Task<ServiceResponse<T>> Insert(T entity, string userName)
        {
            var res = new ServiceResponse<T>();

            var validateResult = await HandleValidate(entity);

            if (validateResult.IsValid)
            {
                var insertedRecord = await _baseDL.Insert(entity, userName);

                if (insertedRecord != null)
                {
                    res.Success = true;
                    res.Data = insertedRecord;
                    res.Message = string.Format(Resource.InsertSuccess, GetTableName(typeof(T).Name));
                }
                else
                {
                    res.Success = false;
                    res.Message = string.Format(Resource.InsertError, GetTableName(typeof(T).Name));
                    res.Error = new ErrorResult
                    {
                        DevMes = string.Format(Resource.NotExist, GetTableName(typeof(T).Name)),
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

        public async Task<ServiceResponse<T>> Update(Guid id, T entity, string userName)
        {
            var res = new ServiceResponse<T>();

            var record = await _baseDL.GetById(id);

            if (record != null)
            {
                // Validate
                var validateResult = await HandleValidate(entity);

                if (validateResult.IsValid)
                {
                    var isSuccess = await _baseDL.Update(id, entity, userName);

                    if (isSuccess)
                    {
                        res.Success = true;
                        res.Data = entity;
                        res.Message = string.Format(Resource.UpdateSuccess, GetTableName(typeof(T).Name));
                    }
                    else
                    {
                        res.Success = false;
                        res.Message = string.Format(Resource.UpdateError, GetTableName(typeof(T).Name));
                        res.Error = new ErrorResult
                        {
                            DevMes = string.Format(Resource.NotExist, GetTableName(typeof(T).Name)),
                            UserMes = Resource.HelpText
                        };
                    }
                }
                else
                {
                    throw new ValidateException(Resource.ValidateError, validateResult.Errors);
                }
            }
            else
            {
                res.Success = false;
                res.Message = string.Format(Resource.InsertError, GetTableName(typeof(T).Name));
                res.Error = new ErrorResult
                {
                    DevMes = string.Format(Resource.NotExist, GetTableName(typeof(T).Name)),
                    UserMes = Resource.HelpText
                };
            }

            return res;
        }

        public async Task<ServiceResponse<List<Guid>>> Delete(List<Guid> ids)
        {
            var res = new ServiceResponse<List<Guid>>();

            string listId = string.Join(",", ids);

            if (await _baseDL.CheckExistIds(listId) != ids.Count)
            {
                res.Success = false;
                res.Message = string.Format(Resource.DeleteError, GetTableName(typeof(T).Name));
                res.Error = new ErrorResult
                {
                    DevMes = "",
                    UserMes = Resource.HelpText
                };
            }
            else
            {
                var result = await _baseDL.Delete(listId);

                if (result)
                {
                    res.Success = true;
                    res.Message = string.Format(Resource.DeleteSuccess, GetTableName(typeof(T).Name));
                    res.Data = ids;
                }
                else
                {
                    res.Success = false;
                    res.Message = Resource.HelpText;
                    res.Error = new ErrorResult
                    {
                        DevMes = "",
                        UserMes = Resource.HelpText
                    };
                }
            }

            return res;
        }

        private static string GetTableName(string name)
        {
            return name.ToLower() switch
            {
                "user" => "thông tin tài khoản",
                "product" => "sản phẩm",
                "address" => "địa chỉ",
                "coupon" => "phiếu giảm giá",
                "category" => "danh mục",
                "supplier" => "nhà cung cấp",
                "news" => "tin tức",
                "account" => "tài khoản",
                "invoice" => "phiếu nhập",
                "order" => "đơn hàng",
                _ => "",
            };
        }

        protected async Task<ValidateResult> HandleValidate(T entity)
        {
            var validateResult = new ValidateResult();
            Dictionary<string, string> messages = new();

            var props = entity.GetType().GetProperties();

            foreach (var prop in props)
            {
                var name = prop.Name;
                var value = prop.GetValue(entity, null);

                var isRequried = prop.IsDefined(typeof(RequiredAttribute), false);
                var hasRegex = prop.IsDefined(typeof(CRegularExpression), false);
                var displayName = prop.GetCustomAttributes(typeof(CDisplayName), false).FirstOrDefault() as CDisplayName;

                if (isRequried && string.IsNullOrEmpty(value.ToString().Trim()))
                {
                    validateResult.IsValid = false;
                    messages.Add(name, string.Format(Resource.EmptyError, displayName.DisplayName));
                }
                else if (hasRegex)
                {
                    var regex = prop.GetCustomAttributes(typeof(CRegularExpression), false).FirstOrDefault() as CRegularExpression;
                    var match = Regex.Match(value.ToString().Trim(), regex.Regex);

                    if (!match.Success)
                    {
                        validateResult.IsValid = false;

                        messages.Add(name, string.Format(Resource.EmptyError, displayName.DisplayName));
                    }
                }
            }

            validateResult.Errors = messages;

            if (validateResult.IsValid)
            {
                validateResult = await CustomValidate(validateResult, entity);
            }


            return validateResult;
        }

        protected virtual async Task<ValidateResult> CustomValidate(ValidateResult validateResult, T entity)
        {
            return validateResult;
        }
    }
}
