namespace BullsAndCows.Web.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using BullsAndCows.Data;
    using BullsAndCows.Models;
    using System;
    using BullsAndCows.Web.DataModels;
    using System.Text;
    using BullsAndCows.GameLogic;
    using BullsAndCows.Web.Infrastructure;

    [Authorize]
    public class GamesController : BaseApiController
    {
        private IGameResultValidator resultValidator;
        private IUserIdProvider userIdProvider;
        private ILogic gameLogic;

        //public GamesController(
        //    IBullsAndCowsData data,
        //    IGameResultValidator resultValidator,
        //    IUserIdProvider userIdProvider,
        //    ILogic gameLogic
        //    )
        //    : base(data)
        //{
        //    this.resultValidator = resultValidator;
        //    this.userIdProvider = userIdProvider;
        //    this.gameLogic = gameLogic;
        //}

        public GamesController()
            : base()
        {
        }

        public GamesController(IBullsAndCowsData data)
            : base(data)
        {
        }

        // NOT Finished
        [Authorize]
        [HttpPost]
        public IHttpActionResult GamesCreate(Game game)
        {
            var currentUserId = this.User.Identity.GetUserId();

            Game newGame = new Game
            {
                RedId = currentUserId,
                //DateCreated = DateTime.Now,
                Name = game.Name,
                Number = game.Number,

            };

            this.data.Games.Add(newGame);
            this.data.SaveChanges();

            return Ok(new
            {
                Id = newGame.Id,
                Name = newGame.Name,
                Blue = newGame.Blue.Name,
                Red = newGame.Red.Name,
                GameState = newGame.State,
                //DateCreated = newGame.DateCreated
            }
            );
        }

        // NOT Finished
        [HttpPut]
        public IHttpActionResult Join()
        {
            var currentUserId = this.userIdProvider.GetUserId();

            var game = this.data.Games
                .All()
                .Where(g => g.State == GameState.WaitingForOpponent && g.RedId != currentUserId)
                .FirstOrDefault();

            if (game == null)
            {
                return NotFound();
            }

            game.BlueId = currentUserId;
            game.State = GameState.BlueInTurn;
            this.data.SaveChanges();

            return Ok(game.Id);
        }
    }
}