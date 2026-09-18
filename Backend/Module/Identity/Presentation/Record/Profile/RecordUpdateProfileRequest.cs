using System.ComponentModel.DataAnnotations;
using Shared.Enum;

namespace Identity.Presentation.Record.Profile
{
    public record RecordUpdateProfileRequest(
        [Required]
        [MaxLength(12)]
        string IdentityCode,
        [Required]
        Guid AccountId,
        [MaxLength(30)]
        string? FirstName,
        [MaxLength(30)]
        string? LastName,
        [DataType(DataType.Date)]
        DateTime? DateOfBirth,
        [MaxLength(10)]
        [DataType(DataType.PhoneNumber)]
        string? PhoneNumber,
        [MaxLength(255)]
        string? Address,
        [MaxLength(255)]
        [DataType(DataType.ImageUrl)]
        string? AvatarUrl,
        ESystemUserGender UserGender = ESystemUserGender.Unspecified
    );
}