namespace MINIMART.Common.Entities.DTO
{
    public class CartDTO
    {
        public Guid AccountId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
