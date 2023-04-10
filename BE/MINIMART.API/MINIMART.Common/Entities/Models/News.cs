namespace MINIMART.Common.Entities.Models
{
    public class News : Base
    {
        public Guid NewsId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Thumbnail { get; set; }
    }
}
