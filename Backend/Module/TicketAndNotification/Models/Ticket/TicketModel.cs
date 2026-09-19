using System.ComponentModel.DataAnnotations;
using TicketAndNotification.Utils.Enum;

namespace TicketAndNotification.Models.Ticket
{
    public class TicketModel
{
    public Guid TicketId { get; set; }

    public Guid AccountId { get; set; }

    [Required, MaxLength(255)] public string Content { get; set; } = string.Empty;

    public ETicketType Type { get; set; }
        = ETicketType.Feedback;

    public bool IsResolved { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

}
}