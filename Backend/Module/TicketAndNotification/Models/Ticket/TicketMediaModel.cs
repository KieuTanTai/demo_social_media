namespace TicketAndNotification.Models.Ticket
{
    public class TicketMediaModel
{
    public int TicketMediaId { get; set; }

    public Guid TicketId { get; set; }

    public string Image { get; set; } = string.Empty;
}
}