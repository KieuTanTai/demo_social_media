using Backend.Module.Contract.Utils.Enum;

namespace Backend.Module.Contract.Models.Contract
{
    public class ContractViolationModel
    {
        public Guid ViolationId { get; set; }

        public Guid ContractId { get; set; }

        public string ViolationContent { get; set; } = null!;

        public decimal? CompensationAmount { get; set; }

        public DateTime ViolationDate { get; set; }

        public EViolationStatus Status { get; set; }
            = EViolationStatus.WaitingConfirmation;


        // Navigation
        public ContractModel Contract { get; set; } = null!;
    }
}