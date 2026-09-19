namespace Backend.Module.Contract.Models.Contract
{
    public class RegulationModel
    {
        public Guid RegulationId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }


        // Navigation
        public ICollection<ContractRegulationModel> ContractRegulations { get; set; }
            = new List<ContractRegulationModel>();
    }
}