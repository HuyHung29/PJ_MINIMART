namespace MINIMART.Common.Entities.DTO
{
    public class PagingResult<T>
    {
        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public int TotalPage { get; set; }

        public int TotalRecord { get; set; }

        public IEnumerable<T>? Data { get; set; }
    }
}
