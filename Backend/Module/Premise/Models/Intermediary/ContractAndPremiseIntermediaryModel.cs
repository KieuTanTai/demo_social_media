using Backend.Module.Premise.Models.Premise;

namespace Backend.Module.Premise.Models.Intermediary
{
    public class RentedPremiseModel
    {
        public Guid ContractId { get; set; }

        public Guid PremiseId { get; set; }

        // Navigation

        public PremiseModel Premisde { get; set; } = null!;
    }
}