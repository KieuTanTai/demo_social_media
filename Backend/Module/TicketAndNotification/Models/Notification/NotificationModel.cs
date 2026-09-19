using Backend.Module.TicketAndNotification.Utils.Enum;

namespace Backend.Module.TicketAndNotification.Models.Notification
{
    public class Notification
    {
        public Guid NotificationId { get; set; }

        public Guid SenderAccountId { get; set; }

        public string ReceiverAccountIds { get; set; } = null!;

        public ENotificationType Type { get; set; }
            = ENotificationType.Other;

        public string Content { get; set; } = null!;

        public ENotificationStatus Status { get; set; }
            = ENotificationStatus.Unread;

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}