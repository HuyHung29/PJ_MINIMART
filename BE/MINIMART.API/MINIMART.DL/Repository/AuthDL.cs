using Dapper;
using MINIMART.Common.Entities.Models;
using MINIMART.DL.Context;
using MINIMART.DL.IRepository;

namespace MINIMART.DL.Repository
{
    public class AuthDL : IAuthDL
    {
        private readonly IContext _context;

        public AuthDL(IContext context)
        {
            _context = context;
        }

        public async Task<Account> GetAccountByUserName(string userName)
        {
            string sql = "Select * from account a where a.UserName = @UserName";

            using (var cnn = _context.CreateConnection())
            {
                var parameters = new DynamicParameters();

                parameters.Add("@UserName", userName);

                var acc = await cnn.QueryFirstOrDefaultAsync<Account>(sql, parameters);

                return acc;
            }

        }
    }
}
