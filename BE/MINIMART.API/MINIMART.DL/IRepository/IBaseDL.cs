namespace MINIMART.DL.IRepository
{
    public interface IBaseDL<T>
    {
        public Task<IEnumerable<T>> GetByFilterAndPaging();
    }
}
