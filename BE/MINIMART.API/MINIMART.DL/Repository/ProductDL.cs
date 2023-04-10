using Dapper;
using MINIMART.Common.Entities.DTO;
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
                    Data = list.ToList(),
                    TotalPage = totalPage,
                    TotalRecord = totalRecord
                };
            }
        }
    }
}
