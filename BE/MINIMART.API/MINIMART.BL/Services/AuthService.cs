using MINIMART.BL.IServices;
using MINIMART.Common;
using MINIMART.Common.Entities.DTO;
using MINIMART.DL.IRepository;
using System.ComponentModel;

namespace MINIMART.BL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthDL _authDL;
        public AuthService(IAuthDL authDL)
        {
            _authDL = authDL;
        }

        public async Task<ServiceResponse<string>> Register(AuthDTO auth)
        {
            var res = new ServiceResponse<string>();

            await ValidateAccount(res, auth);

            return res;
        }

        private async Task ValidateAccount(ServiceResponse<string> res, AuthDTO auth)
        {
            ErrorResult error = new();

            Dictionary<string, string> messages = new();

            var props = auth.GetType().GetProperties();

            foreach (var prop in props)
            {
                var name = prop.Name;
                var value = prop.GetValue(auth, null);

                var displayName = prop.GetCustomAttributes(typeof(DisplayNameAttribute), false).FirstOrDefault();
            }

            if (string.IsNullOrEmpty(auth.UserName))
            {
                messages.Add("UserName", string.Format(Resource.EmptyError, "UserName"));
            }
        }
    }
}
