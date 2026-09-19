using System.ComponentModel.DataAnnotations;

namespace Contract.Models.Contract
{
    public class RegulationModel
    {
        public Guid RegulationId { get; set; }

        [Required, MaxLength(50)] public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Description { get; set; }

        public decimal? FineAmount { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

    }
}