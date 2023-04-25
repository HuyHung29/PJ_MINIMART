using Dapper;
using MINIMART.Common.Entities.DTO;
using MINIMART.Common.Entities.Models;
using MINIMART.Common.Resources;
using MINIMART.DL.Context;
using MINIMART.DL.IRepository;
using System.Data;
using static Dapper.SqlMapper;

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
            string proc = string.Format(ProcResource.GetPaging, typeof(T).Name);

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
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                Data = records.ToList(),
                TotalPage = totalPage,
                TotalRecord = totalRecord
            };
        }

        public async Task<T> GetById(Guid id)
        {
            string sql = "";
            if (typeof(T) == typeof(User))
            {
                sql = $"select * from {typeof(T).Name} where AccountId = @id";

            }
            else
            {
                sql = $"select * from {typeof(T).Name} where {typeof(T).Name}Id = @id";
            }

            var parameters = new DynamicParameters();

            parameters.Add($"@id", id);

            using var cnn = _context.CreateConnection();
            var records = await cnn.QueryFirstOrDefaultAsync<T>(sql, param: parameters);

            return records;

        }

        public async Task<T> Insert(T entity, string userName)
        {
            string proc = string.Format(ProcResource.Insert, typeof(T).Name);

            var parameters = new DynamicParameters();

            foreach (var prop in entity.GetType().GetProperties())
            {
                if (prop.Name == "CreatedBy" || prop.Name == "ModifiedBy")
                {
                    parameters.Add($"p_{prop.Name}", userName);
                    continue;
                }

                parameters.Add($"p_{prop.Name}", prop.GetValue(entity));
            }

            using var cnn = _context.CreateConnection();
            var record = await cnn.QueryFirstOrDefaultAsync<T>(proc, param: parameters, commandType: CommandType.StoredProcedure);

            return record;
        }

        public async Task<bool> Update(Guid id, T entity, string userName)
        {
            string proc = string.Format(ProcResource.Update, typeof(T).Name);

            var parameters = new DynamicParameters();

            foreach (var prop in entity.GetType().GetProperties())
            {
                if (typeof(T) == typeof(User) && prop.Name.Contains("Id"))
                {
                    parameters.Add($"p_AccountId", id);
                    continue;
                }

                if (prop.Name == $"{typeof(T).Name}Id")
                {
                    parameters.Add($"p_{prop.Name}", id);
                    continue;
                }

                if (prop.Name == "CreatedBy" || prop.Name == "ModifiedBy")
                {
                    parameters.Add($"p_{prop.Name}", userName);
                    continue;
                }

                parameters.Add($"p_{prop.Name}", prop.GetValue(entity));
            }

            int rowEffected;
            using (var cnn = _context.CreateConnection())
            {
                rowEffected = await cnn.ExecuteAsync(proc, param: parameters, commandType: CommandType.StoredProcedure);
            }

            return rowEffected > 0;
        }

        public async Task<bool> Delete(string ids)
        {
            string proc = string.Format(ProcResource.Delete, typeof(T).Name);

            var parameters = new DynamicParameters();

            parameters.Add($"p_Ids", ids);

            var rowEffected = 0;

            using (var cnn = _context.CreateConnection())
            {
                cnn.Open();

                using var transaction = cnn.BeginTransaction();
                try
                {
                    rowEffected = await cnn.ExecuteAsync(proc, param: parameters, transaction: transaction, commandType: CommandType.StoredProcedure);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return rowEffected > 0;
        }

        public async Task<int> CheckExistIds(string ids)
        {
            var sql = $"Select Count(*) from {typeof(T).Name} where FIND_IN_SET({typeof(T).Name}Id, @Ids)";

            var parameters = new DynamicParameters();

            parameters.Add("@Ids", ids);

            var numberRow = 0;

            using (var cnn = _context.CreateConnection())
            {
                numberRow = await cnn.QueryFirstOrDefaultAsync<int>(sql, parameters);
            }

            return numberRow;
        }

        public async Task<bool> GetByName(string name, Guid? id)
        {
            string sql = "";

            var parameters = new DynamicParameters();
            if (id != Guid.Empty)
            {
                sql = $"Select Count(*) from {typeof(T).Name} where {typeof(T).Name}Name = @Name And {typeof(T).Name}Id != @Id";
                parameters.Add("@Id", id);
            }
            else
            {
                sql = $"Select Count(*) from {typeof(T).Name} where {typeof(T).Name}Name = @Name";
            }
            parameters.Add("@Name", name);


            var numberRow = 0;

            using (var cnn = _context.CreateConnection())
            {
                numberRow = await cnn.QueryFirstOrDefaultAsync<int>(sql, parameters);
            }

            return numberRow >= 1;
        }

        public async Task<bool> GetByCode(string code, Guid? id)
        {
            string sql = "";
            var parameters = new DynamicParameters();

            if (id != Guid.Empty)
            {
                sql = $"Select Count(*) from {typeof(T).Name} where {typeof(T).Name}Code = @Code And {typeof(T).Name}Id != @Id ";
                parameters.Add("@Id", id);
            }
            else
            {

                sql = $"Select Count(*) from {typeof(T).Name} where {typeof(T).Name}Code = @Code";
            }
            parameters.Add("@Code", code);

            var numberRow = 0;

            using (var cnn = _context.CreateConnection())
            {
                numberRow = await cnn.QueryFirstOrDefaultAsync<int>(sql, parameters);
            }

            return numberRow >= 1;
        }
    }
}
