using MINIMART.Common.Entities.DTO;
using MINIMART.Common.Entities.Models;

namespace MINIMART.BL.IServices
{
    public interface INewsService : IBaseService<News>
    {
        public Task<ServiceResponse<News>> Insert(NewsDTO news, string userName);
        public Task<ServiceResponse<News>> Update(Guid id, NewsDTO news, string userName);
    }
}
