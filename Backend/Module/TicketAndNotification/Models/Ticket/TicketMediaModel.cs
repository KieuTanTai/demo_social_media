namespace Backend.Module.TicketAndNotification.Models.Ticket
{
    public class TicketMediaModel
{
    public Guid TicketMediaId { get; set; }

    public Guid TicketId { get; set; }

    public string Image { get; set; } = null!;


    // Navigation
    public TicketModel Ticket { get; set; } = null!;
}
}