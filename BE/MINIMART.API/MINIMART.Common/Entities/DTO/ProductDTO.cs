using Microsoft.AspNetCore.Http;
using MINIMART.Common.Entities.Models;

namespace MINIMART.Common.Entities.DTO
{
    public class ProductDTO : Product
    {
        public IFormFileCollection? Pictures { get; set; }

        public List<string>? OldPictures { get; set; }
    }
}
