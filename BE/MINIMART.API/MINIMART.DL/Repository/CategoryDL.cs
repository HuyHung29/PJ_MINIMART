using MINIMART.Common.Entities.Models;
using MINIMART.DL.Context;
using MINIMART.DL.IRepository;

namespace MINIMART.DL.Repository
{
    public class CategoryDL : BaseDL<Category>, ICategoryDL
    {
        public CategoryDL(IContext context) : base(context)
        {
        }
    }
}
