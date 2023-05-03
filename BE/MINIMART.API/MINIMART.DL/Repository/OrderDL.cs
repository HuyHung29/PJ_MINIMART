using Dapper;
using MINIMART.Common.Entities.DTO;
using MINIMART.Common.Entities.Models;
using MINIMART.DL.Context;
using MINIMART.DL.IRepository;
using System.Data;

namespace MINIMART.DL.Repository
{
    public class OrderDL : IOrderDL
    {
        private readonly IContext _context;
        public OrderDL(IContext context)
        {
            _context = context;
        }

        public async Task<PagingResult<OrderP>> GetFilterAndPaging(PagingObject filter)
        {
            string proc = "Proc_Order_GetByPaging";

            var parameters = new DynamicParameters();

            foreach (var prop in filter.GetType().GetProperties())
            {
                parameters.Add($"p_{prop.Name}", prop.GetValue(filter));
            }

            parameters.Add("p_Total", DbType.Int32, direction: ParameterDirection.Output);

            using (var cnn = _context.CreateConnection())
            {
                var orderDictionary = new Dictionary<Guid, OrderP>();

                var list = await cnn.QueryAsync<OrderP, ProductP, OrderP>(
                    proc,
                    (order, product) =>
                    {
                        OrderP orderEntry = new();

                        if (!orderDictionary.TryGetValue(order.OrderId, out orderEntry))
                        {
                            orderEntry = order;
                            orderEntry.Products = new List<ProductP>();
                            orderDictionary.Add(orderEntry.OrderId, orderEntry);
                        }

                        if (product != null)
                            orderEntry.Products.Add(product);
                        return orderEntry;
                    },
                    splitOn: "OrderId, ProductId, Quantity",
                    param: parameters, commandType: CommandType.StoredProcedure);

                list = list.GroupBy(x => x.OrderId).Select(x => x.First()).ToList();

                list = list.Select(x =>
                {
                    x.Products = x.Products.GroupBy(y => y.ProductId).Select(y => y.First()).ToList();

                    return x;
                });

                int totalRecord = parameters.Get<int>("p_Total");

                int totalPage = (int)Math.Ceiling((totalRecord * 1.0d) / filter.PageSize);

                return new PagingResult<OrderP>
                {
                    PageNumber = filter.PageNumber,
                    PageSize = filter.PageSize,
                    Data = list.ToList(),
                    TotalPage = totalPage,
                    TotalRecord = totalRecord
                };
            }
        }

        public async Task<List<Product>> GetProductById(string ids)
        {
            string sql = "Select * from product where FIND_IN_SET(ProductId, @Ids);";

            var parameter = new DynamicParameters();

            parameter.Add("@Ids", ids);

            List<Product> products = new();

            using (var cnn = _context.CreateConnection())
            {
                products = (List<Product>)await cnn.QueryAsync<Product>(sql, parameter);
            }

            return products;
        }

        public async Task<bool> ChangeStatus(OrderDTO order)
        {
            string sql = "";

            if (order.Status == Common.Enums.OrderStatus.Delivered)
            {
                sql = "UPDATE `order` o SET o.Status = @Status WHERE o.OrderId = @OrderId AND o.AccountId = @AccountId;";
            }

            if (order.Status == Common.Enums.OrderStatus.Done)
            {
                sql = "UPDATE `order` o SET o.Status = @Status, o.DeliveryDate = Now() WHERE o.OrderId = @OrderId AND o.AccountId = @AccountId;";
            }

            var parameters = new DynamicParameters();

            foreach (var prop in order.GetType().GetProperties())
            {
                if (prop.Name == "Products") continue;
                parameters.Add($"@{prop.Name}", prop.GetValue(order));
            }

            int isSuccedd = 0;

            using (var cnn = _context.CreateConnection())
            {
                isSuccedd = await cnn.ExecuteAsync(sql, parameters);
            }

            return isSuccedd > 0;
        }

        public async Task<bool> Insert(Order order, string ids, string where)
        {
            string values = "";

            foreach (var item in order.Products)
            {
                values += $"('{order.OrderId}', '{item.ProductId}', {item.Quantity}, {item.Price}),";
            }

            values = values.Substring(0, values.Length - 1);

            string sql = "INSERT HIGH_PRIORITY INTO `order` " +
                "(OrderId, OrderCode, AccountId, Total, Status, DeliveryPlace, DeliveryDate, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Receiver, PhoneNumber, Note) " +
                $"VALUES (@OrderId, @OrderCode, @AccountId, @Total, @Status, @DeliveryPlace, @DeliveryDate, @CreatedBy, Now(), @ModifiedBy, Now(), @Receiver, @PhoneNumber, @Note); " +
                "UPDATE product " +
                $"SET ModifiedDate = Now(), " +
                $"Quantity = CASE {where} END " +
                $" WHERE FIND_IN_SET(ProductId, @Ids);" +
                $"INSERT INTO order_detail (OrderId, ProductId, Quantity, Price) VALUES {values};" +
                $"DELETE LOW_PRIORITY QUICK FROM cart WHERE AccountId = @AccountId";

            var parameters = new DynamicParameters();

            foreach (var prop in order.GetType().GetProperties())
            {
                if (prop.Name == "Products") continue;
                parameters.Add($"@{prop.Name}", prop.GetValue(order));
            }

            parameters.Add("@Ids", ids);

            using (var cnn = _context.CreateConnection())
            {
                cnn.Open();
                using var transaction = cnn.BeginTransaction();
                try
                {
                    var result = await cnn.QueryAsync(sql, param: parameters, transaction: transaction);
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                    throw;
                }
            }
        }

        public async Task<Order> GetOrderById(Guid id)
        {
            string sql = "SELECT * FROM `order` o WHERE o.OrderId = @Id";

            var parameter = new DynamicParameters();

            parameter.Add("@Id", id);

            Order order = new();

            using (var cnn = _context.CreateConnection())
            {
                order = await cnn.QueryFirstOrDefaultAsync<Order>(sql, parameter);
            }

            return order;
        }

        public async Task<bool> CancelOrder(OrderDTO order, string ids, string where)
        {
            string sql = "UPDATE `order` o SET o.Status = @Status WHERE o.OrderId = @OrderId AND o.AccountId = @AccountId;" +
                "UPDATE product " +
                $"SET ModifiedDate = Now(), " +
                $"Quantity = CASE {where} END " +
                $" WHERE FIND_IN_SET(ProductId, @Ids);";

            var parameters = new DynamicParameters();

            foreach (var prop in order.GetType().GetProperties())
            {
                if (prop.Name == "Products") continue;
                parameters.Add($"@{prop.Name}", prop.GetValue(order));
            }

            parameters.Add("@Ids", ids);

            using (var cnn = _context.CreateConnection())
            {
                cnn.Open();
                using var transaction = cnn.BeginTransaction();
                try
                {
                    var result = await cnn.QueryAsync(sql, param: parameters, transaction: transaction);
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                    throw;
                }
            }
        }
    }
}
