using MINIMART.Common.Entities.DTO;

namespace MINIMART.DL.IRepository
{
    public interface IBaseDL<T>
    {
        public Task<PagingResult<T>> GetByFilterAndPaging(PagingObject filter);

        public Task<T> GetById(Guid id);

        public Task<bool> GetByName(string name, Guid? id);

        public Task<bool> GetByCode(string code, Guid? id);

        public Task<int> CheckExistIds(string ids);

        public Task<T> Insert(T entity, string userName);

        public Task<bool> Update(Guid id, T entity, string userName);

        public Task<bool> Delete(string ids);
    }
}
