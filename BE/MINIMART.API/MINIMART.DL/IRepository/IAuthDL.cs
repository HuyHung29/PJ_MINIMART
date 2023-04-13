using MINIMART.Common.Entities.Models;

namespace MINIMART.DL.IRepository
{
    public interface IAuthDL
    {
        public Task<Account> GetAccountByUserName(string userName);

        public Task<User> GetUserById(Guid id);

        public Task<Guid> Insert(Account acc);

        public Task<Guid> Update(Account acc);

        public Task<Guid> Delete(Guid id);

        public Task<bool> Active(Guid id);
    }
}
