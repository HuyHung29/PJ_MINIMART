using System.ComponentModel.DataAnnotations;
using static MINIMART.Common.Entities.Attributes.CustomAtrribute;

namespace MINIMART.Common.Entities.Models
{
    public class News
    {
        public Guid NewsId { get; set; }

        [Required]
        [CDisplayName("Tiêu đề")]
        public string? Title { get; set; }

        [Required]
        [CDisplayName("Nội dung")]
        public string? Description { get; set; }

        public string? Thumbnail { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
