using MINIMART.BL.IServices;
using MINIMART.Common.Entities.Models;
using MINIMART.DL.IRepository;

namespace MINIMART.BL.Services
{
    public class NewsService : BaseService<News>, INewsService
    {
        public NewsService(IBaseDL<News> baseDL) : base(baseDL)
        {
        }
    }
}
