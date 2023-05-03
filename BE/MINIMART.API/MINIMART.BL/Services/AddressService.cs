using MINIMART.BL.IServices;
using MINIMART.Common.Entities.Models;
using MINIMART.DL.IRepository;

namespace MINIMART.BL.Services
{
    public class AddressService : BaseService<Address>, IAddressService
    {
        public AddressService(IBaseDL<Address> baseDL) : base(baseDL)
        {
        }
    }
}
