using System.ComponentModel.DataAnnotations;

namespace Identity.Presentation.Record.Profile
{
    public record RecordCreateBaseProfileRequest(
        [Required]
        Guid AccountId,
        [Required]
        [StringLength(12)]
        [DataType(DataType.Text)]
        string IdentityCode);
}