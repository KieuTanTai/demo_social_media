using System.ComponentModel.DataAnnotations;

namespace Contract.Models.Invoice
{
    public class MonthlyInvoiceModel
    {
        public Guid InvoiceId { get; set; }

        public Guid ContractId { get; set; }

        public DateTime? PaymentDate { get; set; }

        public DateTime? DueDate { get; set; }

        public decimal? TotalAmount { get; set; }

        [Required, MaxLength(10)] public string Status { get; set; } = "unpaid";

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

    }
}