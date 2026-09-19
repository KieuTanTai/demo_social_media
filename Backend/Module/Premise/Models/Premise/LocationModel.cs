namespace Backend.Module.Premise.Models.Premise
{
    public class LocationModel
    {
        public Guid LocationId { get; set; }

        public string Address { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }


        // Navigation
        public ICollection<PremiseModel> Premises { get; set; }
            = new List<PremiseModel>();

        
    }
}