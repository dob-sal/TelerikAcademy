namespace BullsAndCows.Web.Controllers
{
    using System.Web.Http;
    using System.Linq;

    using Data;
    using Model;
    using Web.Infrastructure;
    using ResponseModels;

    public class ScoresController : BaseController
    {
        private const int DefaultNumberOfUsersInLeaderBoard = 10;
        public ScoresController(IBullsAndCowsData data)
            : base(data)
        {
        }

        [HttpGet]
        public IHttpActionResult GetRankings()
        {
            var result = this.Database.Users.All()
                .Select(RankResponseModel.FromUser)
                .OrderByDescending(u => u.Rank)
                .ThenBy(u => u.Username)
                .Take(DefaultNumberOfUsersInLeaderBoard);

            return Ok(result);
        }
    }
}