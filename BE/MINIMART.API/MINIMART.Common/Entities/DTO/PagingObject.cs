using MINIMART.Common.Enums;

namespace MINIMART.Common.Entities.DTO
{
    public class PagingObject
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 20;

        public string? ColSort { get; set; }

        public SortOrder? SortOrder { get; set; }

        public string? Filter { get; set; }

        public int? Status { get; set; }

        public Guid? SupplierId { get; set; } = Guid.Empty;

        public Guid? CategoryId { get; set; } = Guid.Empty;

        public Guid? ProductId { get; set; } = Guid.Empty;

        public Guid? OrderId { get; set; } = Guid.Empty;

        public Guid? InvoiceId { get; set; } = Guid.Empty;

        public Guid? NewsId { get; set; } = Guid.Empty;

        public Guid? AccountId { get; set; } = Guid.Empty;

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string? ColDate { get; set; }
    }
}
