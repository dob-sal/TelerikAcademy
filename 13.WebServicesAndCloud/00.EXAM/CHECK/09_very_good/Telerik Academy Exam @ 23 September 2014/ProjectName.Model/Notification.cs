namespace BullsAndCows.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Notification
    {
        public int Id { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime DateCreated { get; set; }

        public string PlayerId { get; set; }

        public virtual User Player { get; set; }

        public int GameId { get; set; }

        public virtual Game Game { get; set; }

        public NotificationState State { get; set; }

        public NotificationType Type { get; set; }

    }
}
