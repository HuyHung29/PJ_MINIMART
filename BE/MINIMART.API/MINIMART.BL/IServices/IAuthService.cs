using MINIMART.Common.Entities.DTO;

namespace MINIMART.BL.IServices
{
    public interface IAuthService
    {
        public Task<ServiceResponse<string>> Register(AuthDTO auth);
    }
}
