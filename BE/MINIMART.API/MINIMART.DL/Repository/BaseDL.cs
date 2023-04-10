using Dapper;
using MINIMART.Common.Entities.DTO;
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

        public virtual async Task<PagingResult<T>> GetByFilterAndPaging(PagingObject filter)
        {
            string proc = $"Proc_{typeof(T).Name}_GetByPaging";

            var parameters = new DynamicParameters();

            foreach (var prop in filter.GetType().GetProperties())
            {
                parameters.Add($"p_{prop.Name}", prop.GetValue(filter));
            }

            parameters.Add("p_Total", "", DbType.Int32, direction: ParameterDirection.Output);

            IEnumerable<T> records;

            using (var cnn = _context.CreateConnection())
            {
                records = await cnn.QueryAsync<T>(proc, param: parameters, commandType: CommandType.StoredProcedure);
            }

            int totalRecord = parameters.Get<int>("p_Total");

            int totalPage = (int)Math.Ceiling((totalRecord * 1.0d) / filter.PageSize);

            return new PagingResult<T>
            {
                Data = records.ToList(),
                TotalPage = totalPage,
                TotalRecord = totalRecord
            };
        }

        public Task<T> Insert(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
