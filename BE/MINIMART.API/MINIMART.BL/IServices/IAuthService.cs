using MINIMART.Common.Entities.DTO;

namespace MINIMART.BL.IServices
{
    public interface IAuthService
    {
        public Task<ServiceResponse<Guid>> Register(AuthDTO auth);

        public Task<bool> Active(Guid accId);

        public Task<ServiceResponse<LoginResult>> Login(LoginDTO auth);
    }
}
