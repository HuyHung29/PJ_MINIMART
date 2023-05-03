using Dapper;
using MINIMART.Common.Entities.DTO;
using MINIMART.Common.Entities.Models;
using MINIMART.DL.Context;
using MINIMART.DL.IRepository;
using System.Data;

namespace MINIMART.DL.Repository
{
    public class CartDL : BaseDL<CartDTO>, ICartDL
    {
        public CartDL(IContext context) : base(context)
        {
        }

        public async Task<PagingResult<Product>> GetByFilter(Guid accId, PagingObject filter)
        {

            string proc = "Proc_Cart_GetByPaging";

            var parameters = new DynamicParameters();

            foreach (var prop in filter.GetType().GetProperties())
            {
                parameters.Add($"p_{prop.Name}", prop.GetValue(filter));
            }

            parameters.Add("p_Total", DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("p_AccountId", accId);

            using (var cnn = _context.CreateConnection())
            {
                var productDictionary = new Dictionary<Guid, Product>();

                var list = await cnn.QueryAsync<Product, Picture, Product>(
                    proc,
                    (product, picture) =>
                    {
                        Product productEntry = new();

                        if (!productDictionary.TryGetValue(product.ProductId, out productEntry))
                        {
                            productEntry = product;
                            productEntry.Pictures = new List<Picture>();
                            productDictionary.Add(productEntry.ProductId, productEntry);
                        }

                        if (picture != null)
                            productEntry.Pictures.Add(picture);
                        return productEntry;
                    },
                    splitOn: "ProductId, PictureId",
                    param: parameters, commandType: CommandType.StoredProcedure);

                list = list.GroupBy(x => x.ProductId).Select(x => x.First()).ToList();

                int totalRecord = parameters.Get<int>("p_Total");

                int totalPage = (int)Math.Ceiling((totalRecord * 1.0d) / filter.PageSize);

                return new PagingResult<Product>
                {
                    PageNumber = filter.PageNumber,
                    PageSize = filter.PageSize,
                    Data = list.ToList(),
                    TotalPage = totalPage,
                    TotalRecord = totalRecord
                };
            }
        }

        public async Task<bool> InsertCart(CartDTO cartDTO)
        {
            string proc = "Proc_Cart_Insert";

            var parameters = new DynamicParameters();

            foreach (var prop in cartDTO.GetType().GetProperties())
            {
                parameters.Add($"p_{prop.Name}", prop.GetValue(cartDTO));
            }

            int result;

            using (var cnn = _context.CreateConnection())
            {
                result = await cnn.ExecuteAsync(proc, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
            }

            return result > 0;
        }

        public async Task<bool> UpdateCart(CartDTO cartDTO)
        {
            string proc = "Proc_Cart_Update";

            var parameters = new DynamicParameters();

            foreach (var prop in cartDTO.GetType().GetProperties())
            {
                parameters.Add($"p_{prop.Name}", prop.GetValue(cartDTO));
            }

            int result;

            using (var cnn = _context.CreateConnection())
            {
                result = await cnn.ExecuteAsync(proc, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
            }

            return result > 0;
        }

        public Task<Cart> Insert(Cart entity, string userName)
        {
            throw new NotImplementedException();
        }


        public Task<bool> Update(Guid id, Cart entity, string userName)
        {
            throw new NotImplementedException();
        }

        Task<PagingResult<Cart>> IBaseDL<Cart>.GetByFilterAndPaging(PagingObject filter)
        {
            throw new NotImplementedException();
        }

        Task<Cart> IBaseDL<Cart>.GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteCart(Guid accId, string ids)
        {
            string proc = "Proc_Cart_Delete";

            var parameters = new DynamicParameters();

            parameters.Add($"p_Ids", ids);
            parameters.Add("p_AccountId", accId);

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

        public async Task<bool> CheckCart(CartDTO item)
        {
            string proc = "Select count(*) from cart where AccountId = @accId and ProductId = @proId";

            var parameters = new DynamicParameters();

            parameters.Add("@accId", item.AccountId);
            parameters.Add("@proId", item.ProductId);


            int result;

            using (var cnn = _context.CreateConnection())
            {
                result = await cnn.QueryFirstOrDefaultAsync<int>(proc, param: parameters);
            }

            return result > 0;
        }
    }
}
