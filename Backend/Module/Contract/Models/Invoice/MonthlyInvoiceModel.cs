using Backend.Module.Contract.Models.Contract;

namespace Backend.Module.Contract.Models.Invoice
{
    public class MonthlyInvoiceModel
    {
        public Guid InvoiceId { get; set; }

        public Guid ContractId { get; set; }

        public DateTime PaymentDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }


        // Navigation
        public ContractModel Contract { get; set; } = null!;

        public ICollection<InvoiceDetailModel> InvoiceDetails { get; set; }
            = new List<InvoiceDetailModel>();
    }
}