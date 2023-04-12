using MINIMART.Common.Entities.DTO;
using MINIMART.Common.Entities.Models;

namespace MINIMART.BL.IServices
{
    public interface IAuthService
    {
        public Task<ServiceResponse<Guid>> Register(AuthDTO auth);

        public Task<bool> Active(Guid accId);

        public Task<User> Login(AuthDTO auth);
    }
}
