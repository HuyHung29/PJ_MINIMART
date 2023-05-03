using MINIMART.BL.IServices;
using MINIMART.Common.Entities.DTO;
using MINIMART.Common.Entities.Models;
using MINIMART.Common.Enums;
using MINIMART.Common.Resources;
using MINIMART.DL.IRepository;

namespace MINIMART.BL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderDL _orderDL;


        public OrderService(IOrderDL orderDL)
        {
            _orderDL = orderDL;
        }

        public async Task<ServiceResponse<bool>> ChangeStatus(OrderDTO order)
        {
            var res = new ServiceResponse<bool>();
            var oldOrder = await _orderDL.GetOrderById(order.OrderId);

            if (oldOrder != null)
            {
                if (oldOrder.Status == OrderStatus.Waiting && order.Status == OrderStatus.Cancled)
                {
                    var ids = order.Products.Select(p => p.ProductId).ToList();

                    string listId = string.Join(",", ids);

                    var listPro = await _orderDL.GetProductById(listId);

                    string where = "";

                    for (int i = 0; i < listPro.Count; i++)
                    {
                        int newQuantity = listPro[i].Quantity + order.Products[i].Quantity;

                        where += $"WHEN ProductId = '{listPro[i].ProductId}' THEN {newQuantity} ";
                    }

                    var ressult = await _orderDL.CancelOrder(order, listId, where);

                    if (ressult)
                    {
                        res.Success = true;
                        res.Message = "Hủy đơn hàng thành công";

                        return res;
                    }
                    else
                    {
                        res.Success = false;
                        res.Message = Resource.HelpText;
                    }
                }

                if (order.Status == OrderStatus.Waiting)
                {
                    res.Success = false;
                    res.Message = "Không cho phép đổi trạng thái về đang chờ";
                    return res;
                }
                else
                {
                    var isSuccess = await _orderDL.ChangeStatus(order);

                    if (isSuccess)
                    {
                        res.Success = true;
                        res.Message = "Thay đổi trạng thái đơn hàng thành công";
                        return res;
                    }
                    else
                    {
                        res.Success = false;
                        res.Message = Resource.HelpText;
                    }
                }
            }
            else
            {
                res.Success = false;
                res.Message = Resource.HelpText;
            }

            return res;
        }

        public async Task<PagingResult<OrderP>> GetFilterAndPaging(PagingObject filter)
        {
            return await _orderDL.GetFilterAndPaging(filter);
        }

        public async Task<ServiceResponse<bool>> Insert(Order order)
        {
            var res = new ServiceResponse<bool>();
            res.Success = true;
            order.OrderId = Guid.NewGuid();

            var ids = order.Products.Select(p => p.ProductId).ToList();

            string listId = string.Join(",", ids);

            var listPro = await _orderDL.GetProductById(listId);

            decimal? total = 0;
            string where = "";

            for (int i = 0; i < listPro.Count; i++)
            {
                if (listPro[i].Quantity < order.Products[i].Quantity)
                {
                    res.Success = false;
                    res.Message = "Sản phẩm bạn chọn đã hết hàng";
                    break;
                }
                total += listPro[i].CurrentPrice * order.Products[i].Quantity;

                int newQuantity = listPro[i].Quantity - order.Products[i].Quantity;

                where += $"WHEN ProductId = '{listPro[i].ProductId}' THEN {newQuantity} ";
            }

            if (res.Success)
            {
                order.Total = total;

                var result = await _orderDL.Insert(order, listId, where);

                if (result)
                {
                    res.Success = true;
                    res.Message = "Đặt hàng thành công";
                }
                else
                {
                    res.Success = false;
                    res.Message = Resource.HelpText;
                }

            }

            return res;
        }
    }
}
