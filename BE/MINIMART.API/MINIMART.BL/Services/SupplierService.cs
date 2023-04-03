using MINIMART.BL.IServices;
using MINIMART.Common.Entities.Models;
using MINIMART.DL.IRepository;

namespace MINIMART.BL.Services
{
    public class SupplierService : BaseService<Supplier>, ISupplierService
    {
        public SupplierService(IBaseDL<Supplier> baseDL) : base(baseDL)
        {
        }
    }
}
