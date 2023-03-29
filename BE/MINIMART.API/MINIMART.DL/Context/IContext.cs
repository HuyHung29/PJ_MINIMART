using System.Data;

namespace MINIMART.DL.Context
{
    public interface IContext
    {
        public IDbConnection CreateConnection();
    }
}
