using MINIMART.BL.IServices;
using MINIMART.Common.Entities.DTO;
using MINIMART.Common.Entities.Models;
using MINIMART.Common.Exceptions;
using MINIMART.Common.Resources;
using MINIMART.DL.IRepository;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using static MINIMART.Common.Entities.Attributes.CustomAtrribute;

namespace MINIMART.BL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthDL _authDL;

        public AuthService(IAuthDL authDL)
        {
            _authDL = authDL;
        }

        public async Task<ServiceResponse<Guid>> Register(AuthDTO auth)
        {
            var res = new ServiceResponse<Guid>();

            var validateResult = await ValidateAccount(auth);

            if (validateResult.IsValid)
            {
                using var hmac = new HMACSHA512();

                var acc = new Account
                {
                    UserName = auth.UserName,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(auth.Password)),
                    PasswordSalt = hmac.Key
                };

                var result = await _authDL.Insert(acc);

                if (result != Guid.Empty)
                {
                    res.Success = true;
                    res.Message = "Đăng ký thành công";
                }
                else
                {
                    res.Success = false;
                    res.Error = new ErrorResult
                    {
                        DevMes = "Có lỗi khi thêm mới tài khoản",
                        UserMes = Resource.HelpText
                    };
                }

                res.Data = result;

                return res;
            }
            else
            {
                throw new ValidateException(Resource.ValidateError, validateResult.Errors);
            }

        }

        public async Task<bool> Active(Guid accId)
        {
            var result = await _authDL.Active(accId);

            return result;
        }

        private async Task<ValidateResult> ValidateAccount(AuthDTO auth)
        {
            var validateResult = new ValidateResult();
            Dictionary<string, string> messages = new();

            var props = auth.GetType().GetProperties();

            foreach (var prop in props)
            {
                var name = prop.Name;
                var value = prop.GetValue(auth, null);

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

            if (validateResult.IsValid)
            {
                var acc = await _authDL.GetAccountByUserName(auth.UserName);

                if (acc != null)
                {
                    validateResult.IsValid = false;
                    messages.Add("UserName", string.Format(Resource.IsExisted, "Tên đăng nhập"));
                }
            }

            validateResult.Errors = messages;

            return validateResult;
        }

        public Task<User> Login(AuthDTO auth)
        {
            throw new NotImplementedException();
        }
    }
}
