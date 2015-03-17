namespace BullsAndCows.Web.Controllers
{
    using System.Web.Http;
    using System.Linq;

    using Data;
    using Model;
    using Web.Infrastructure;
    using ResponseModels;

    [Authorize]
    public class NotificationsController : BaseController
    {
        private const int DefaultNumberOfGamesPerPage = 10;

        private IUserIdProvider userIdProvider;

        public NotificationsController(IBullsAndCowsData data, IUserIdProvider userIdProvider)
            :base(data)
        {
            this.userIdProvider = userIdProvider;
        }

        [HttpGet]
        public IHttpActionResult GetAll(int page)
        {
            var userId = this.userIdProvider.GetUserId();

            var notificationsInDb = this.Database.Notifications.All()
                .Where(n => n.PlayerId == userId)
                .OrderBy(n => n.DateCreated)
                .Skip(DefaultNumberOfGamesPerPage * page)
                .Take(DefaultNumberOfGamesPerPage);
            
            var result = notificationsInDb.Select(NotificationResponseModel.FromNotification).ToList();

            foreach (var notification in notificationsInDb)
            {
                notification.State = NotificationState.Read;
            }

            this.Database.SaveChanges();

            return Ok(result);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            return this.GetAll(0);
        }

        [Route("api/notifications/next")]
        [HttpGet]
        public IHttpActionResult GetNext()
        {
            var userId = this.userIdProvider.GetUserId();

            var notification = this.Database.Notifications.All()
                .Where(n => n.PlayerId == userId && n.State == NotificationState.Unread)
                .OrderBy(n => n.DateCreated);

            var result = notification.Select(NotificationResponseModel.FromNotification).FirstOrDefault();
            notification.FirstOrDefault().State = NotificationState.Read;
            this.Database.SaveChanges();
            if (result != null)
            {
                return Ok(result);
            }
            return Ok("You have no unread messages!");
        }
    }
}