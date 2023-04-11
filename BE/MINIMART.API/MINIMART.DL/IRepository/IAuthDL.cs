using MINIMART.Common.Entities.Models;

namespace MINIMART.DL.IRepository
{
    public interface IAuthDL
    {
        public Task<Account> GetAccountByUserName(string userName);
    }
}
