using MINIMART.Common.Entities.DTO;

namespace MINIMART.BL.IServices
{
    public interface IBaseService<T>
    {
        public Task<PagingResult<T>> GetByFilterAndPaging(PagingObject filter);

        public Task<ServiceResponse<T>> Insert(T entity);

        public Task<ServiceResponse<T>> Update(Guid id, T entity);

        public Task<ServiceResponse<List<Guid>>> Delete(List<Guid> ids);
    }
}
