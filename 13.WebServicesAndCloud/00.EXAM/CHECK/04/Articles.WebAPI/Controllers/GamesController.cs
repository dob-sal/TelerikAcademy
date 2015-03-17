namespace Articles.WebAPI.Controllers
{
    using Articles.CompeteLogic;
    using Articles.Data;
    using Articles.Models;
    using Articles.WebAPI.Models;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    [Authorize]
    public class GamesController : BaseApiController
    {
        private const int defaultPageSize = 10;
        private GameLogic logic;

        public GamesController(IArticlesData data)
            : base(data)
        {
            this.logic = new GameLogic();
        }

        //[Route("api/games")]
        //[HttpGet]
        //public IHttpActionResult GetAuthGamesFirst()
        //{

        //    return BadRequest("not implemented yet first page"); //testing


        //}

        //[HttpGet]
        //public IHttpActionResult GetAuthGamesPage([FromUri]int page)
        //{

        //    return BadRequest("not implemented yet by Page"); //testing


        //}

        //[HttpGet]
        //public IHttpActionResult GetGame(int id)
        //{

        //    return BadRequest("not implemented yet by SpecificID");
        //}

        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Get(0);
        }

        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult Get([FromUri]int page)
        {
            var games = GetAllGamesSortedForUnauthorized()
                .Skip(page * defaultPageSize)
                .Take(defaultPageSize);

            return Ok(games);
        }

        private IEnumerable<GameDataModel> GetAllGamesSortedForUnauthorized()
        {
            return this.data.Games.All()
                .OrderBy(a => a.State)
                .ThenBy(a => a.Name)
                .ThenBy(a => a.DateCreated)
                .ThenBy(a => a.RedPlayer)
                .Select(GameDataModel.FromGame);
        }

        [HttpPost]
        public IHttpActionResult Create(GameCreateModel inputModel)
        {
            if (!logic.IsValidNumber(inputModel.RedNumber))
            {
                return BadRequest("invalid number for Red player");
            }

            var userID = this.User.Identity.GetUserId();

            var newGame = new Game
            {
                Name = inputModel.Name,
                RedPlayerID = userID,
                RedNumber = inputModel.RedNumber,
                DateCreated = DateTime.Now,
                State = GameState.WaitingForOpponent
            };

            this.data.Games.Add(newGame);
            this.data.SaveChanges();

            var userName = this.User.Identity.GetUserName();
            var stateString = newGame.State.ToString();

            var returnModel = new
            {
                ID = newGame.ID,
                Name = newGame.Name,
                Blue = "No blue player yet",
                Red = userName,
                State = stateString,
                DateCreated = newGame.DateCreated,
            };

            return Ok(returnModel);
        }

        [HttpPut]
        public IHttpActionResult Join(int id, [FromBody]GameJoinModel model)
        {
            var currentGame = this.data.Games.Find(id);

            if (currentGame.State != GameState.WaitingForOpponent)
            {
                return BadRequest("Game is not available for joining");
            }

            var blueNumber = model.Number;

            if (!logic.IsValidNumber(blueNumber))
            {
                return BadRequest("invalid number for Blue player");
            }

            currentGame.BluePlayerID = this.User.Identity.GetUserId();
            currentGame.BlueNumber = blueNumber;
            
            if (logic.RedGoesFirst())
            {
                currentGame.State = GameState.RedInTurn;
            }
            else
            {
                currentGame.State = GameState.BlueInTurn;
            }
            var gameName = currentGame.Name;
            this.data.SaveChanges();

            //{"result": "You joined game \"The Empire strikes back!\""}
            string msg = string.Format("You joined game\"{0}\"", gameName);
            var returnModel = new
            {
                result = msg
            };
            return Ok(returnModel);
        }

        //[Route("api/games/{id}/guess")]
        //[HttpPost]
        //public IHttpActionResult PostGuess(int gameId)
        //{

        //    return BadRequest("not implemented Guesses yet");
        //}
    }
}
