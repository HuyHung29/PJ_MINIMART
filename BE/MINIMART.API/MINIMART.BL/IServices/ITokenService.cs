using MINIMART.Common.Entities.Models;

namespace MINIMART.BL.IServices
{
    public interface ITokenService
    {
        public string CreateToken(Account acc);
    }
}
