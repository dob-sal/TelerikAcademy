namespace BullsAndCows.Web.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using BullsAndCows.Data;
    using BullsAndCows.Models;
    using BullsAndCows.Web.DataModels;

    public class NotificationsController : ApiController
    {
        private IBullsAndCowsData data;

        public NotificationsController(IBullsAndCowsData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var currentUserName = User.Identity.GetUserName();

            var notoficationDataModel = this.data.Notifications.All().
                Where(n => n.User.UserName == currentUserName).
                OrderBy(n => n.DateCreated).
                Take(10).
                Project().
                To<NotificationInfoDataModel>();

            return Ok(notoficationDataModel);
        }
    }
}