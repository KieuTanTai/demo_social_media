namespace Backend.Module.Contract.Models.Contract
{
    public class ContractRegulationModel
    {
        public Guid RegulationId { get; set; }

        public Guid ContractId { get; set; }


        // Navigation
        public RegulationModel Regulation { get; set; } = null!;

        public ContractModel Contract { get; set; } = null!;
    }
}