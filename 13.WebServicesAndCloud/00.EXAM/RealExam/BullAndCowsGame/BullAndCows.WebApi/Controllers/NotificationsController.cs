using BullAndCows.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Microsoft.AspNet.Identity;

namespace BullAndCows.WebApi.Controllers
{
    public class NotificationsController : BaseApiController
    {

        public NotificationsController()
            : base()
        {
        }

        public NotificationsController(IBullAndCowsData data)
            : base(data)
        {
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult GetNotifications()
        {
            var userID = this.User.Identity.GetUserId();

            var notifications = this.data.Notifications
                                         .All()
                                         .Where(n => n.UserID == userID)
                                         .Select(n =>
                                          new
                                          {
                                              Id = n.Id,
                                              Message = n.Message,
                                              DateCreated = n.DateCreated,
                                              Type = n.Type,
                                              State = n.State,
                                              GameId = n.GameId
                                          }

                                      );

            return Ok(notifications);
        }
    }
}
