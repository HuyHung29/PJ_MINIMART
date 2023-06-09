﻿using MINIMART.BL.IServices;
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
        private readonly ITokenService _tokenService;

        public AuthService(IAuthDL authDL, ITokenService tokenService)
        {
            _authDL = authDL;
            _tokenService = tokenService;
        }

        public async Task<ServiceResponse<Guid>> Register(AuthDTO auth)
        {
            var res = new ServiceResponse<Guid>();

            var validateResult = await ValidateAccount(auth);

            if (validateResult.IsValid)
            {
                using var hmac = new HMACSHA512();


                auth.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(auth.Password));
                auth.PasswordSalt = hmac.Key;

                var result = await _authDL.Insert(auth);

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

                var email = await _authDL.GetUserByEmail(auth.Email);

                if (email != null)
                {
                    validateResult.IsValid = false;
                    messages.Add("Email", string.Format(Resource.IsExisted, "Email"));
                }
            }

            validateResult.Errors = messages;

            return validateResult;
        }

        public async Task<ServiceResponse<LoginResult>> Login(LoginDTO auth)
        {
            var res = new ServiceResponse<LoginResult>();

            var acc = await _authDL.GetAccountByUserName(auth.UserName);

            var validateResult = ValidateLogin(auth, acc);

            if (validateResult.IsValid)
            {
                var user = await _authDL.GetUserById(acc.AccountId);
                res.Success = true;
                res.Data = new LoginResult
                {
                    User = user,
                    AccessToken = _tokenService.CreateToken(acc)
                };
                res.Message = Resource.LoginSuccess;
            }
            else
            {
                res.Success = false;
                res.Message = Resource.LoginFail;
                res.Error = new ErrorResult
                {
                    DevMes = Resource.LoginFail,
                    MoreInfo = validateResult.Errors
                };
            }

            return res;
        }

        private ValidateResult ValidateLogin(LoginDTO auth, Account acc)
        {
            var result = new ValidateResult();

            Dictionary<string, string> mess = new();

            if (acc == null)
            {
                result.IsValid = false;
                mess.Add("UserName", "Tên đăng nhập không chính xác");
            }
            else
            {
                using var hmac = new HMACSHA512(acc.PasswordSalt);

                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(auth.Password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != acc.PasswordHash[i])
                    {
                        result.IsValid = false;
                        mess.Add("Password", "Mật khẩu không chính xác");
                        break;
                    }
                }
            }

            result.Errors = mess;

            return result;
        }

        public async Task<ServiceResponse<bool>> ChangePassword(Guid id, string password, string newPassword)
        {
            var res = new ServiceResponse<bool>();
            var acc = await _authDL.GetAccountById(id);

            if (acc == null)
            {
                res.Success = false;
                res.Message = "Tài khoản không tồn tại";
            }
            else
            {
                bool isValid = true;
                using var hmac = new HMACSHA512(acc.PasswordSalt);

                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != acc.PasswordHash[i])
                    {
                        isValid = false;
                        res.Message = "Mật khẩu không chính xác";
                        break;
                    }
                }

                if (isValid)
                {
                    Account newAcc = new();
                    using var hmaca = new HMACSHA512();

                    newAcc.AccountId = id;
                    newAcc.PasswordHash = hmaca.ComputeHash(Encoding.UTF8.GetBytes(newPassword));
                    newAcc.PasswordSalt = hmaca.Key;

                    var isUpdate = await _authDL.Update(newAcc);

                    if (isUpdate)
                    {
                        res.Success = true;
                        res.Message = "Thay đổi mật khẩu thành công";
                        res.Data = true;
                    }
                    else
                    {
                        res.Success = false;
                        res.Message = Resource.HelpText;
                    }
                }
            }

            return res;
        }
    }
}
