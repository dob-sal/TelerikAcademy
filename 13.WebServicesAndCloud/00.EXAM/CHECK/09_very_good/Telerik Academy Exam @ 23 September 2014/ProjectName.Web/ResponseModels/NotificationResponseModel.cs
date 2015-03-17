namespace BullsAndCows.Web.ResponseModels
{
    using System;
    using System.Linq.Expressions;
    
    using BullsAndCows.Model;
    public class NotificationResponseModel
    {
        public static Expression<Func<Notification, NotificationResponseModel>> FromNotification
        {
            get
            {
                return x => new NotificationResponseModel
                {
                    Id = x.Id,
                    Message = x.Message,
                    DateCreated = x.DateCreated,
                    State = x.State.ToString(),
                    Type = x.Type.ToString(),
                    GameId = x.GameId,
                };
            }
        }

        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime DateCreated { get; set; }

        public string State { get; set; }
        
        public string Type { get; set; }

        public int GameId { get; set; }
    }
}