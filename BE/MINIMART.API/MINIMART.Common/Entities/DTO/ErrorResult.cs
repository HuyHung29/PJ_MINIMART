using System.Collections;

namespace MINIMART.Common.Entities.DTO
{
    public class ErrorResult
    {
        public string? Devmes { get; set; }

        public string? UserMess { get; set; }

        public IDictionary? MoreInfo { get; set; }
    }
}
