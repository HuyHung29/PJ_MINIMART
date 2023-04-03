using Dapper;
using MINIMART.DL.Context;
using MINIMART.DL.IRepository;
using System.Data;

namespace MINIMART.DL.Repository
{
    public class BaseDL<T> : IBaseDL<T>
    {
        protected readonly IContext _context;

        public BaseDL(IContext context)
        {
            _context = context;
        }

        public virtual async Task<IEnumerable<T>> GetByFilterAndPaging()
        {
            string proc = $"Proc_{typeof(T).Name}_GetByPaging";

            var parameters = new DynamicParameters();

            parameters.Add("p_PageSize", 10, DbType.Int16);
            parameters.Add("p_PageNumber", 1, DbType.Int16);
            parameters.Add("p_Filter", "", DbType.String);

            IEnumerable<T> records;

            using (var cnn = _context.CreateConnection())
            {
                records = await cnn.QueryAsync<T>(proc, param: parameters, commandType: CommandType.StoredProcedure);
            }
            return records.ToList();
        }
    }
}
