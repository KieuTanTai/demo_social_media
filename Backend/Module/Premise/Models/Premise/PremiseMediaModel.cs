namespace Backend.Module.Premise.Models.Premise
{
    public class PremiseMediaModel
    {
        public int PremiseMediaId { get; set; }

        public string Image { get; set; } = null!;

        public Guid PremiseId { get; set; }


        // Navigation
        public PremiseModel Premise { get; set; } = null!;
    }
}