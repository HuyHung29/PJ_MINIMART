using MINIMART.BL.IServices;
using MINIMART.Common.Entities.DTO;
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

        public async Task<PagingResult<T>> GetByFilterAndPaging(PagingObject filter)
        {
            var result = await _baseDL.GetByFilterAndPaging(filter);

            return result;
        }

        public async Task<ServiceResponse<T>> Insert(T entity)
        {
            // Prepare Data

            // Validate

            // Do insert
            var result = await _baseDL.Insert(entity);

            return new ServiceResponse<T>
            {
                Success = true,
                Data = result
            };
        }
    }
}
