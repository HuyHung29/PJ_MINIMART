using System.Collections;

namespace MINIMART.Common.Entities.DTO
{
    public class ErrorResult
    {
        public string? DevMes { get; set; }

        public string? UserMes { get; set; }

        public IDictionary? MoreInfo { get; set; }
    }
}
