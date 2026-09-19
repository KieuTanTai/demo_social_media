using System.ComponentModel.DataAnnotations;

namespace TicketAndNotification.Models.Notification
{
    public class Notification
    {
        public Guid NotificationId { get; set; }

        public Guid SenderAccountId { get; set; }

        public TicketAndNotification.Utils.Enum.ENotificationType Type { get; set; }
            = TicketAndNotification.Utils.Enum.ENotificationType.Other;

        [Required, MaxLength(255)] public string Content { get; set; } = string.Empty;

        public bool IsRead { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}