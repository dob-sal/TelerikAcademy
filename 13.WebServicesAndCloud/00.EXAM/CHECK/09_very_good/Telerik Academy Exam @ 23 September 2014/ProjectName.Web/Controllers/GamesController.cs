namespace BullsAndCows.Web.Controllers
{
    using System;
    using System.Text;
    using System.Linq;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using Data;
    using Model;
    using GameLogic;
    using Infrastructure;
    using ResponseModels;
    using RequestModels;
    using BullsAndCows.Web.ResponseModels.Colors;

    public class GamesController : BaseController
    {
        private const int DefaultNumberOfGamesPerPage = 10;
        private static Random random = new Random();

        private IGameNumberValidator numberValidator;
        private IUserIdProvider userIdProvider;
        private IUserNameProvider userNameProvider;

        public GamesController(
            IBullsAndCowsData data,
            IGameNumberValidator numberValidator,
            IUserIdProvider userIdProvider,
            IUserNameProvider userNameProvider)
            :base(data)
        {
            this.numberValidator = numberValidator;
            this.userIdProvider = userIdProvider;
            this.userNameProvider = userNameProvider;
        }


        [HttpGet]
        public IHttpActionResult AllGames(int page)
        {
            var games = this.Database.Games.All()
                .OrderBy(g => g.State)
                .ThenBy(g => g.Name)
                .ThenBy(g => g.DateOfCreation)
                .ThenBy(g => g.RedPlayer.UserName)
                .Skip(DefaultNumberOfGamesPerPage * page)
                .Take(DefaultNumberOfGamesPerPage)
                .Select(GameResponseModel.FromGame);

            return Ok(games);
        }

        [HttpGet]
        public IHttpActionResult AllGames()
        {
            return this.AllGames(0);
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult GameById(int id)
        {
            var userId = this.userIdProvider.GetUserId();
            var game = this.Database.Games.All().FirstOrDefault(g => g.Id == id);
            if (game == null)
            {
                return BadRequest("Invalid game id!");
            }

            if (game.RedPlayerId != userId && game.BluePlayerId != userId)
            {
                return BadRequest("You are not authorized to view this game!");
            }

            if (game.State != GameState.BlueInTurn || game.State != GameState.RedInTurn)
            {
                return BadRequest("Game is not currently played");
            }

            bool isCurentPlayerRed = game.RedPlayerId == userId;

            var resultGame = new GameByIdResponseModel()
            {
                Id = game.Id,
                Blue = game.BluePlayer.UserName ?? "No blue player yet",
                DateCreated = game.DateOfCreation,
                GameState = game.State,
                Name = game.Name,
                Red = game.RedPlayer.UserName,
            };

            if (isCurentPlayerRed)
            {
                resultGame.YourColor = Color.red;
                resultGame.YourNumber = game.RedPlayerNumber;
                resultGame.OpponentGuesses = this.Database.Guesses.All().Where(g => g.UserId == game.BluePlayerId && g.GameId == game.Id).Select(GuessResponseModel.FromGuess).ToList();
                resultGame.YourGuesses = this.Database.Guesses.All().Where(g => g.UserId == userId && g.GameId == game.Id).Select(GuessResponseModel.FromGuess).ToList();
            }
            else
            {
                resultGame.YourColor = Color.blue;
                resultGame.YourNumber = game.BluePlayerNumber;
                resultGame.OpponentGuesses = this.Database.Guesses.All().Where(g => g.UserId == game.RedPlayerId && g.GameId == game.Id).Select(GuessResponseModel.FromGuess).ToList();
                resultGame.YourGuesses = this.Database.Guesses.All().Where(g => g.UserId == userId && g.GameId == game.Id).Select(GuessResponseModel.FromGuess).ToList();
            }

            return Ok(resultGame);
        }
        //[HttpGet]
        //[Authorize]
        //public IHttpActionResult AllGamesAuthorized()
        //{
        //    return this.AllGamesAuthorized(0);
        //}

        //[HttpGet]
        //[Authorize]
        //public IHttpActionResult AllGamesAuthorized(int page)
        //{
        //    var userId = this.userIdProvider.GetUserId();

        //    var games = this.Database.Games.All()
        //        .Where(
        //        g =>
        //            // User is part of but are not finished
        //            (g.RedPlayerId == userId || g.BluePlayerId == userId) && (g.State == GameState.BlueInTurn || g.State == GameState.RedInTurn)
        //            ||
        //            // Available for joining
        //            (g.State == GameState.WaitingForOpponent)
        //            )
        //        .OrderBy(g => g.State)
        //        .ThenBy(g => g.Name)
        //        .ThenBy(g => g.DateOfCreation)
        //        .ThenBy(g => g.RedPlayer.UserName)
        //        .Skip(DefaultNumberOfGamesPerPage * page)
        //        .Take(DefaultNumberOfGamesPerPage)
        //        .Select(GameResponseModel.FromGame);

        //    return Ok(games);
        //}

        [Authorize]
        [HttpPut]
        public IHttpActionResult JoinGame(int id, JoinRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentGame = this.Database.Games.All().FirstOrDefault(g => g.Id == id);

            if (currentGame == null)
            {
                return BadRequest("Invalid game id!");
            }

            if (currentGame.State != GameState.WaitingForOpponent)
            {
                return BadRequest("You cannot join this game!");
            }

            if (!this.numberValidator.IsValidGameNumber(model.Number))
            {
                return BadRequest("Invalid user number"); 
            }

            var userId = this.userIdProvider.GetUserId();

            if (currentGame.RedPlayerId == userId)
            {
                return BadRequest("You cannot join your own game!");
            }

            // Join the game
            currentGame.BluePlayerId = userId;
            currentGame.BluePlayerNumber = model.Number;
            
            
            currentGame.State = random.Next() % 2 == 0 ? GameState.RedInTurn : GameState.BlueInTurn;
            this.Database.Notifications.Add(new Notification()
            {
                DateCreated = DateTime.Now,
                Message = "It is your turn in game \"" + currentGame.Name + "\"",
                PlayerId = currentGame.State == GameState.RedInTurn ? currentGame.RedPlayerId : currentGame.BluePlayerId,
                Type = NotificationType.YourTurn,
                State = NotificationState.Unread,
                GameId = currentGame.Id
            });
            
            // Notify Red
            var redNotification = new Notification()
            {
                DateCreated = DateTime.Now,
                Message = this.userNameProvider.GetUserName() + "joined your game\"" + currentGame.Name + "\"",
                GameId = currentGame.Id,
                State = NotificationState.Unread,
                Type = NotificationType.GameJoined,
                PlayerId = currentGame.RedPlayerId,
            };

            this.Database.Notifications.Add(redNotification);

            this.Database.SaveChanges();

            // Return
            var result = new JoinResponseModel()
            {
                Result = "You joined game \"" + currentGame.Name + "\""
            };

            return Ok(result);
        }
        

        [Authorize]
        [HttpPost]
        public IHttpActionResult CreateGame(GameRequestModel gameModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (!this.numberValidator.IsValidGameNumber(gameModel.Number))
            {
                return BadRequest("Invalid user number");
            }
            
            var userId = this.userIdProvider.GetUserId();
            var game = new Game()
            {
                Name = gameModel.Name,
                RedPlayerId = userId,
                RedPlayerNumber = gameModel.Number,
                DateOfCreation = DateTime.Now,
                State = GameState.WaitingForOpponent
            };

            this.Database.Games.Add(game);
            this.Database.SaveChanges();

            var resultGame = this.Database.Games.All().Where(g => g.Name == gameModel.Name).Select(GameResponseModel.FromGame).First();

            return Created<GameResponseModel>("", resultGame);
        }
    }
}