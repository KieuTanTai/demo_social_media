using Backend.Module.TicketAndNotification.Utils.Enum;

namespace Backend.Module.TicketAndNotification.Models.Ticket
{
    public class TicketModel
{
    public Guid TicketId { get; set; }

    public Guid AccountId { get; set; }

    public string Content { get; set; } = null!;

    public ETicketType Type { get; set; }
        = ETicketType.Feedback;

    public ETicketStatus Status { get; set; }
        = ETicketStatus.Received;

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }


    // Navigation
    public ICollection<TicketMediaModel> TicketMedias { get; set; }
        = new List<TicketMediaModel>();
}
}