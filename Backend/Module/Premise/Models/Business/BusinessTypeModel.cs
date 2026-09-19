using Backend.Module.Premise.Models.Premise;
using Backend.Module.Premise.Models.Product;

namespace Backend.Module.Premise.Models.Business
{
    public class BusinessTypeModel
    {
        public Guid BusinessTypeId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        


        // Navigation
        public ICollection<PremiseBusinessTypeModel> PremiseBusinessTypes { get; set; }
            = new List<PremiseBusinessTypeModel>();

        public ICollection<ProductBusinessTypeModel> ProductBusinessTypes { get; set; }
            = new List<ProductBusinessTypeModel>();

        
    }

}