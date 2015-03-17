using ServicesExam.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ServicesExam.Services.Models
{
    public class NotificationOutput
    {
        public int Id { get; set; }

        public string MessageContent { get; set; }
        public DateTime DateCreated { get; set; }

        public string Type { get; set; }

        public string State { get; set; }

        public int GameId { get; set; }

        //public virtual Game Game { get; set; }

        public string UserId { get; set; }

        public static Expression<Func<Message, NotificationOutput>> FromMessage
        {
            get
            {
                return notification => new NotificationOutput
                {
                    DateCreated = notification.DateCreated,
                    GameId = notification.GameId,
                    Id = notification.Id,
                    MessageContent = notification.MessageContent,
                    State = notification.State.ToString(),
                    Type = notification.Type.ToString(),
                    UserId = notification.UserId
                };
            }
        }
    }
}