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

    public class GamesController : ApiController
    {
        private readonly IBullsAndCowsData data;
        private BullsAndCowsDbContext context;

        public GamesController(IBullsAndCowsData data)
        {
            this.data = data;
            this.context = new BullsAndCowsDbContext();
            this.UserManager = new UserManager<GameUser>(new UserStore<GameUser>(this.context));
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var gameDataModel = this.data.Games.All().Where(g => g.State == GameState.AvailableForJoining).
                OrderBy(g => g.State).
                ThenBy(g => g.Name).
                ThenBy(g => g.DateCreated).
                ThenBy(g => g.Red.UserName).
                Take(10).
                Project().
                To<GameInfoDataModel>();

            return Ok(gameDataModel);
        }

        //[HttpGet]
        //public IHttpActionResult GetByPage(int page)
        //{
        //    var gameDataModel = this.data.Games.All().
        //        Where(g => g.State == GameState.AvailableForJoining).
        //        OrderBy(g => g.State).
        //        ThenBy(g => g.Name).
        //        ThenBy(g => g.DateCreated).
        //        ThenBy(g => g.Red.UserName).
        //        Skip(10 * page).
        //        Take(10).
        //        Project().
        //        To<GameInfoDataModel>();

        //    return Ok(gameDataModel);
        //}


        [Authorize]
        [HttpGet]
        public IHttpActionResult GetByUserId(int page)
        {
            var currentUserID = this.User.Identity.GetUserId();

            var gameDataModel = this.data.Games.All().
                Where(g => g.State == GameState.AvailableForJoining || g.RedId == currentUserID || g.BlueId == currentUserID).
                OrderBy(g => g.State).
                ThenBy(g => g.Name).
                ThenBy(g => g.DateCreated).
                ThenBy(g => g.Red.UserName).
                Skip(10 * page).
                Take(10).
                Project().
                To<GameInfoDataModel>();

            return Ok(gameDataModel);
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GameById(int id)
        {
            var currentUserID = this.User.Identity.GetUserId();

            var gameDataModel = this.data.Games.All().
                Where(g => g.Id == id && (g.RedId == currentUserID || g.BlueId == currentUserID)).
                OrderBy(g => g.State).
                ThenBy(g => g.Name).
                ThenBy(g => g.DateCreated).
                ThenBy(g => g.Red.UserName).
                Take(10).
                Project().
                To<GameInfoDataModel>();

            return Ok(gameDataModel);
        }

        [HttpPost]
        public IHttpActionResult Create(CreateGameDataModel model)
        {
            var userId = User.Identity.GetUserId();
            var redName = User.Identity.GetUserName();
            var user = new GameUser { UserNumber = model.Number, UserName = redName };

            var currentUser = data.Users.All().FirstOrDefault(u => u.Id == userId);

            currentUser.UserNumber = model.Number;

          //  this.data.Users.Add(user);

            this.data.SaveChanges();

            var game = new Game
            {
                Red = currentUser,
                RedId = userId,
                Name = model.Name,
            };

            game.Red.UserNumber = model.Number;
            game.Red.UserName = redName;

            this.data.Games.Add(game);
            this.data.SaveChanges();

            var gameDataModel =
                this.data.Games.All()
                    .Where(x => x.Id == game.Id)
                    .Project()
                    .To<GameInfoDataModel>()
                    .FirstOrDefault();

            return this.Ok(gameDataModel);
        }

        public UserManager<GameUser> UserManager { get; set; }
    }
}