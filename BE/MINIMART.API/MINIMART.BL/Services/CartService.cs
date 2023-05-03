using MINIMART.BL.IServices;
using MINIMART.Common.Entities.DTO;
using MINIMART.Common.Entities.Models;
using MINIMART.Common.Resources;
using MINIMART.DL.IRepository;

namespace MINIMART.BL.Services
{
    public class CartService : BaseService<Cart>, ICartService
    {
        private readonly IProductDL _productDL;
        private readonly ICartDL _cartDL;
        public CartService(ICartDL cartDL, IProductDL productDL) : base(cartDL)
        {
            _productDL = productDL;
            _cartDL = cartDL;
        }

        public async Task<PagingResult<Product>> GetByFilter(Guid accId, PagingObject filter)
        {
            var result = await _cartDL.GetByFilter(accId, filter);

            return result;
        }

        public async Task<ServiceResponse<List<Guid>>> Delete(Guid accId, List<Guid> proIds)
        {
            var res = new ServiceResponse<List<Guid>>();

            string listId = string.Join(",", proIds);

            if (accId != Guid.Empty)
            {
                var result = await _cartDL.DeleteCart(accId, listId);

                if (result)
                {
                    res.Success = true;
                    res.Message = "Xóa sản phẩm thành công";
                    res.Data = proIds;
                }
                else
                {
                    res.Success = false;
                    res.Message = "Xóa sản phẩm thất bại";
                }
            }
            else
            {
                res.Success = false;
                res.Message = "Xóa sản phẩm thất bại";
            }


            return res;
        }

        public async Task<ServiceResponse<Cart>> Insert(Guid id, CartDTO item)
        {
            var res = new ServiceResponse<Cart>();
            item.AccountId = id;
            var pro = await _productDL.GetById(item.ProductId);
            var isExist = await _cartDL.CheckCart(item);

            if (pro != null)
            {
                if (isExist)
                {
                    res.Success = false;
                    res.Message = "Sản phẩm đã có trong giỏ hàng";
                }
                else
                {
                    if (pro.Quantity < item.Quantity)
                    {
                        res.Success = false;
                        res.Message = "Số lượng mua vượt qua số lượng trong kho";
                    }
                    else
                    {
                        var isSuccess = await _cartDL.InsertCart(item);

                        if (isSuccess)
                        {
                            res.Success = true;
                            res.Message = "Thêm sản phẩm vào giỏ hàng thành công";
                        }
                        else
                        {
                            res.Success = false;
                            res.Message = Resource.HelpText;
                        }
                    }
                }
            }
            else
            {
                res.Success = false;
                res.Message = "Sản phẩm không tồn tại";
            }

            return res;
        }

        public async Task<ServiceResponse<Cart>> Update(Guid id, CartDTO item)
        {
            var res = new ServiceResponse<Cart>();
            item.AccountId = id;
            var pro = await _productDL.GetById(item.ProductId);
            var isExist = await _cartDL.CheckCart(item);

            if (pro != null)
            {
                if (isExist)
                {
                    if (pro.Quantity < item.Quantity)
                    {
                        res.Success = false;
                        res.Message = "Số lượng mua vượt qua số lượng trong kho";
                    }
                    else
                    {
                        var isSuccess = await _cartDL.UpdateCart(item);

                        if (isSuccess)
                        {
                            res.Success = true;
                            res.Message = "Cập nhật số lượng sản phẩm vào giỏ hàng thành công";
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
                    res.Message = "Sản phẩm chưa có trong giỏ hàng";
                }
            }
            else
            {
                res.Success = false;
                res.Message = "Sản phẩm không tồn tại";
            }

            return res;
        }
    }
}
