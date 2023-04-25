using MINIMART.BL.IServices;
using MINIMART.Common.Entities.Models;
using MINIMART.DL.IRepository;

namespace MINIMART.BL.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(IBaseDL<User> baseDL) : base(baseDL)
        {
        }
    }
}
