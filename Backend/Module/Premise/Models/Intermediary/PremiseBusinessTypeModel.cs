using Backend.Module.Premise.Models.Business;

namespace Backend.Module.Premise.Models.Premise
{
    public class PremiseBusinessTypeModel
    {
        public Guid PremiseId { get; set; }

        public Guid BusinessTypeId { get; set; }


        // Navigation
        public PremiseModel Premise { get; set; } = null!;

        public BusinessTypeModel BusinessType { get; set; } = null!;
    }
}