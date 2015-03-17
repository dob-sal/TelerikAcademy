namespace BullsAndCows.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Notification
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [ForeignKey("Game")]
        public int GameId { get; set; }

        public virtual Game Game { get; set; }

        public DateTime DateCreated { get; set; }

        public NotificationType Type { get; set; }

        public NotificationState State { get; set; }

        public string Message
        {
            get
            {
                string message = "";

                switch (this.Type)
                {
                    case NotificationType.GameJoined:
                        message = string.Format(@"{0} joined your game '{1} by {2}'",
                            this.Game.BluePlayer.UserName, this.Game.Name, this.Game.RedPlayer.UserName);
                        break;
                    case NotificationType.YourTurn:
                        message = string.Format(@"It is your turn in game '{0} by {1}'",
                            this.Game.Name, this.Game.RedPlayer.UserName);
                        break;
                    case NotificationType.GameWon:
                        break;
                    case NotificationType.GameLost:
                        break;
                    default:
                        break;
                }

                return message;
            }
        }
    }
}
