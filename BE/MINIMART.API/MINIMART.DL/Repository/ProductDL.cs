using Dapper;
using MINIMART.Common.Entities.Models;
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

        public override async Task<IEnumerable<Product>> GetByFilterAndPaging()
        {

            string proc = "Proc_Product_GetByPaging";

            using (var cnn = _context.CreateConnection())
            {
                var productDictionary = new Dictionary<Guid, Product>();

                var parameters = new DynamicParameters();

                parameters.Add("p_PageSize", 10, DbType.Int16);
                parameters.Add("p_PageNumber", 1, DbType.Int16);

                var list = await cnn.QueryAsync<Product, Picture, Product>(
                    proc,
                    (product, picture) =>
                    {
                        Product productEntry;

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
                    param: parameters,
                    splitOn: "ProductId, PictureId", commandType: CommandType.StoredProcedure);

                list = list.GroupBy(x => x.ProductId).Select(x => x.First()).ToList();
                return list;
            }
        }
    }
}
