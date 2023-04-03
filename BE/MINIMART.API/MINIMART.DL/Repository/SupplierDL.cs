using MINIMART.Common.Entities.Models;
using MINIMART.DL.Context;
using MINIMART.DL.IRepository;

namespace MINIMART.DL.Repository
{
    public class SupplierDL : BaseDL<Supplier>, ISupplierDL
    {
        public SupplierDL(IContext context) : base(context)
        {
        }
    }
}
