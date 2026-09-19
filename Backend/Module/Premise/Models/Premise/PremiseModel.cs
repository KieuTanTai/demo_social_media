using Backend.Module.Premise.Models.Intermediary;
using Backend.Module.Premise.Utils.Enum;

namespace Backend.Module.Premise.Models.Premise
{
    public class PremiseModel
    {
        public Guid PremiseId { get; set; }

        public string Name { get; set; } = null!;

        public Guid LocationId { get; set; }

        public EPremiseStatus Status { get; set; }
            = EPremiseStatus.Available;

        public int Position { get; set; }

        public int Floor { get; set; }

        public decimal Area { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }


        // Navigation
        public LocationModel Location { get; set; } = null!;

        public ICollection<PremiseBusinessTypeModel> PremiseBusinessTypes { get; set; }
            = new List<PremiseBusinessTypeModel>();

        public ICollection<RentedPremiseModel> RentedPremises { get; set; }
            = new List<RentedPremiseModel>();

        public ICollection<PremiseMediaModel> Media { get; set; }
            = new List<PremiseMediaModel>();

    }
}