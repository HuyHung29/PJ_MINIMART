using MINIMART.Common.Entities.DTO;

namespace MINIMART.DL.IRepository
{
    public interface IBaseDL<T>
    {
        public Task<PagingResult<T>> GetByFilterAndPaging(PagingObject filter);

        public Task<T> GetById(Guid id);

        public Task<int> CheckExistIds(string ids);

        public Task<T> Insert(T entity);

        public Task<bool> Update(Guid id, T entity);

        public Task<bool> Delete(string ids);
    }
}
