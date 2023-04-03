using MINIMART.BL.IServices;
using MINIMART.DL.IRepository;

namespace MINIMART.BL.Services
{
    public class BaseService<T> : IBaseService<T>
    {
        protected IBaseDL<T> _baseDL;

        public BaseService(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }

        public async Task<IEnumerable<T>> GetByFilterAndPaging()
        {
            var result = await _baseDL.GetByFilterAndPaging();

            return result;
        }
    }
}
