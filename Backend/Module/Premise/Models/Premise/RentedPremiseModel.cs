namespace Premise.Models.Premise
{
    public class RentedPremiseModel
    {
        public RentedPremiseModel() {}

        public RentedPremiseModel(Guid contractId, Guid premiseId)
        {
            ContractId = contractId;
            PremiseId = premiseId;
        }

        public Guid ContractId { get; init; }

        public Guid PremiseId { get; init; }
    }
}