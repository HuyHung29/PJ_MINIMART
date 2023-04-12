using Dapper;
using MINIMART.Common.Entities.Models;
using MINIMART.Common.Resources;
using MINIMART.DL.Context;
using MINIMART.DL.IRepository;
using System.Data;

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

        public async Task<Guid> Insert(Account acc)
        {
            string proc = string.Format(ProcResource.Insert, typeof(Account).Name);

            var parameters = new DynamicParameters();

            foreach (var prop in acc.GetType().GetProperties())
            {
                if (prop.Name.ToLower().Contains("id"))
                {
                    parameters.Add($"p_{prop.Name}", direction: ParameterDirection.Output);
                }
                else
                {
                    parameters.Add($"p_{prop.Name}", prop.GetValue(acc));
                }
            }

            var rowEffected = 0;

            using (var cnn = _context.CreateConnection())
            {
                rowEffected = await cnn.ExecuteAsync(proc, parameters, commandType: CommandType.StoredProcedure);
            }

            var id = parameters.Get<Guid>("p_AccountId");

            return rowEffected > 0 ? id : Guid.Empty;
        }

        public Task<Guid> Update(Account acc)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Active(Guid id)
        {
            string sql = "UPDATE LOW_PRIORITY account SET IsActive = 1 WHERE AccountId = @AccountId";

            var parameters = new DynamicParameters();

            parameters.Add("@AccountId", id);

            var result = 0;

            using (var cnn = _context.CreateConnection())
            {
                result = await cnn.ExecuteAsync(sql, parameters);
            }

            return result > 0;
        }

        public Task<Guid> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
