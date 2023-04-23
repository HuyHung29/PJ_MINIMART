using Dapper;
using MINIMART.Common.Entities.DTO;
using MINIMART.Common.Entities.Models;
using MINIMART.Common.Resources;
using MINIMART.DL.Context;
using MINIMART.DL.IRepository;
using System.Data;

namespace MINIMART.DL.Repository
{
    public class ProductDL : BaseDL<Product>, IProductDL
    {
        public ProductDL(IContext context) : base(context)
        {
        }

        public override async Task<PagingResult<Product>> GetByFilterAndPaging(PagingObject filter)
        {

            string proc = "Proc_Product_GetByPaging";

            var parameters = new DynamicParameters();

            foreach (var prop in filter.GetType().GetProperties())
            {
                parameters.Add($"p_{prop.Name}", prop.GetValue(filter));
            }

            parameters.Add("p_Total", DbType.Int32, direction: ParameterDirection.Output);

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

        public async Task<Product> Insert(ProductDTO product, string userName)
        {
            string proc = string.Format(ProcResource.Insert, typeof(Product).Name);
            var parameters = new DynamicParameters();

            foreach (var prop in product.GetType().GetProperties())
            {
                if (prop.Name == "Pictures") continue;

                if (prop.Name == "CreatedBy" || prop.Name == "ModifiedBy")
                {
                    parameters.Add($"p_{prop.Name}", userName);
                    continue;
                }

                parameters.Add($"p_{prop.Name}", prop.GetValue(product));
            }

            using var cnn = _context.CreateConnection();
            var record = await cnn.QueryFirstOrDefaultAsync<Product>(proc, param: parameters, commandType: CommandType.StoredProcedure);

            return record;
        }

        public async Task<bool> InsertPicture(List<Picture> pictures)
        {
            string sql = "INSERT INTO picture (ProductId, PictureId, Url) VALUES (@ProductId, @PictureId, @Url)";

            int rowEffected = 0;

            using (var cnn = _context.CreateConnection())
            {
                cnn.Open();

                using var transaction = cnn.BeginTransaction();

                try
                {
                    rowEffected = await cnn.ExecuteAsync(sql, pictures, transaction: transaction);
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

        public async Task<bool> DeletePicture(Guid id)
        {
            string sql = "Delete from picture where ProductId = @Id";

            int rowEffected = 0;
            var parameters = new DynamicParameters();

            parameters.Add("@Id", id);

            using (var cnn = _context.CreateConnection())
            {
                cnn.Open();

                using var transaction = cnn.BeginTransaction();

                try
                {
                    rowEffected = await cnn.ExecuteAsync(sql, param: parameters, transaction: transaction);
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

        public async Task<Product> UpdateProduct(ProductDTO product, Guid id, string userName)
        {
            string proc = string.Format(ProcResource.Update, typeof(Product).Name);
            var parameters = new DynamicParameters();

            foreach (var prop in product.GetType().GetProperties())
            {
                if (prop.Name == "ProductId")
                {
                    parameters.Add($"p_{prop.Name}", id);
                }

                if (prop.Name == "CreatedBy" || prop.Name == "ModifiedBy")
                {
                    parameters.Add($"p_{prop.Name}", userName);
                    continue;
                }

                if (prop.Name == "Pictures") continue;

                parameters.Add($"p_{prop.Name}", prop.GetValue(product));
            }

            using var cnn = _context.CreateConnection();
            var record = await cnn.QueryFirstOrDefaultAsync<Product>(proc, param: parameters, commandType: CommandType.StoredProcedure);

            return record;
        }
    }
}
