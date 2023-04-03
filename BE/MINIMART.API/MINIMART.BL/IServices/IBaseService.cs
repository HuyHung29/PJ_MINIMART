namespace MINIMART.BL.IServices
{
    public interface IBaseService<T>
    {
        public Task<IEnumerable<T>> GetByFilterAndPaging();
    }
}
