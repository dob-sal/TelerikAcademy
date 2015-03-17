namespace ServicesExam.Services.Controllers
{
    using ServicesExam.Data;
    using ServicesExam.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Microsoft.AspNet.Identity;
    using ServicesExam.Services.Models;
    public class NotificationsController :BaseController
    {
        public NotificationsController(IServicesExamData data)
            : base(data)
        {

        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult Get(int page)
        {
            var notificationsPerPage = 10;
            var userId = this.User.Identity.GetUserId();
            var notifications = this.data.Messages.All().Where(x => x.UserId == userId).Skip(page * notificationsPerPage).Take(notificationsPerPage);
            var output = new HashSet<NotificationOutput>();
            foreach (var notification in notifications)
            {
                var notificationOutput = new NotificationOutput
                {
                    DateCreated = notification.DateCreated,
                    GameId = notification.GameId,
                    Id = notification.Id,
                    MessageContent = notification.MessageContent,
                    State = notification.State.ToString(),
                    Type = notification.Type.ToString(),
                    UserId = notification.UserId

                };
                output.Add(notificationOutput);
            }
            return Ok(output);
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult Next()
        {
            var notification = this.data.Messages.All().OrderByDescending(x => x.DateCreated).FirstOrDefault(x => x.State == MessageState.Unread);
            notification.State = MessageState.Read;
            this.data.SaveChanges();

            var output = new NotificationOutput
            {
                DateCreated = notification.DateCreated,
                GameId = notification.GameId,
                Id = notification.Id,
                MessageContent = notification.MessageContent,
                State = notification.State.ToString(),
                Type = notification.Type.ToString(),
                UserId = notification.UserId
            };

            return Ok(output);
        }
    }
}
