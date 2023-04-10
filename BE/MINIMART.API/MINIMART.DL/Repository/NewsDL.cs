using MINIMART.Common.Entities.Models;
using MINIMART.DL.Context;
using MINIMART.DL.IRepository;

namespace MINIMART.DL.Repository
{
    public class NewsDL : BaseDL<News>, INewsDL
    {
        public NewsDL(IContext context) : base(context)
        {
        }
    }
}
