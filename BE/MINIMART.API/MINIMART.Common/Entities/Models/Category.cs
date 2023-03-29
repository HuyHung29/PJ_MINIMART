using System.ComponentModel.DataAnnotations;

namespace MINIMART.Common.Entities.Models
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }

        [Required]  
        public string? CategoryName { get; set; }

        public string? Thumbnail { get; set; }
    }
}
