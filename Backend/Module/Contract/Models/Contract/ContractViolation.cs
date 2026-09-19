using System.ComponentModel.DataAnnotations;

namespace Contract.Models.Contract
{
    public class ContractViolationModel
    {
        public Guid ViolationId { get; set; }

        public Guid ContractId { get; set; }

        [Required, MaxLength(150)] public string ViolationContent { get; set; } = string.Empty;

        public decimal? CompensationAmount { get; set; }

        public DateTime? ViolationDate { get; set; }

        public DateTime? DueDate { get; set; }

        public bool IsResolved { get; set; }

    }
}