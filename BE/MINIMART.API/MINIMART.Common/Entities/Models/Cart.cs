namespace MINIMART.Common.Entities.Models
{
    public class Cart
    {
        public Guid AccountId { get; set; }

        public Product? Products { get; set; }
    }
}
