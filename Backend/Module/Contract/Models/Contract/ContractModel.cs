using Contract.Utils.Enum;

namespace Contract.Models.Contract
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


    }
}