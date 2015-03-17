namespace BullsAndCows.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using BullsAndCows.Data;
    using BullsAndCows.Models;

    [Authorize]
    public class GamesController : ApiController 
    {
        public static Random  random = new Random();

        private IBullsAndCowsData data;

        public GamesController()
            : this(new BullsAndCowsData(new BullsAndCowsDbContext()))
        {
        }

        public GamesController(IBullsAndCowsData data)
        {
            this.data = data;
        }

        [HttpPost]
        public IHttpActionResult Create()
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentUserId = this.User.Identity.GetUserId();

            var newGame = new Game
            {
                RedPlayerId = currentUserId,
                DateCreated = DateTime.Now,
            };

            this.data.Games.Add(newGame);
            this.data.SaveChanges();


            return Ok(newGame);
        }

        [HttpPut]
        public IHttpActionResult Join(int id)
        {
            var currentUserId = this.User.Identity.GetUserId();

            var game = this.data.Games
                .All()
                .Where(g => g.State == GameState.WaitingForOpponent && g.RedPlayerId != currentUserId && g.Id==id)
                .FirstOrDefault();

            if (game == null)
            {
                return NotFound();
            }

            game.BluePlayerId = currentUserId;
            if (random.Next(2) == 0)
            {
                game.State = GameState.TurnBluePlayer;
            }
            else
            {
                game.State = GameState.TurnRedPlayer;
            }
            this.data.SaveChanges();

            string result = "You joined game \"The Empire strikes back!\"";

            return Ok(result);
        }
    }
}