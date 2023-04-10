using MINIMART.Common.Entities.DTO;

namespace MINIMART.DL.IRepository
{
    public interface IBaseDL<T>
    {
        public Task<PagingResult<T>> GetByFilterAndPaging(PagingObject filter);

        public Task<T> Insert(T entity);
    }
}
