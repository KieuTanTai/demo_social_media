using Backend.Module.Contract.Utils.Enum;
using Backend.Module.Contract.Models.Contract;
using Backend.Module.Contract.Models.Invoice;

namespace Backend.Module.Contract.Models.Contract
{
    public class ContractModel
    {
        public Guid ContractId { get; set; }

        public Guid AccountId { get; set; }

        public decimal Deposit { get; set; }

        public decimal RentalPrice { get; set; }

        public DateTime PremiseReturnDate { get; set; }

        public EContractStatus Status { get; set; }
            = EContractStatus.PendingSignature;

        public DateTime? TerminationDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }


        // Navigation

        public ICollection<ContractRegulationModel> ContractRegulations { get; set; }
            = new List<ContractRegulationModel>();

        public ICollection<ContractViolationModel> ContractViolations { get; set; }
            = new List<ContractViolationModel>();

        public ICollection<MonthlyInvoiceModel> MonthlyInvoices { get; set; }
            = new List<MonthlyInvoiceModel>();
    }
}