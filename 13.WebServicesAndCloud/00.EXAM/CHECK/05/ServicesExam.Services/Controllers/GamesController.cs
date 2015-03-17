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

namespace ServicesExam.Services.Controllers
{
    public class GamesController : BaseController
    {
        public GamesController(IServicesExamData data)
            : base(data)
        {

        }


        private IQueryable<Game> GetAllFreeToJoinGames()
        {
            var freeToJoinGames = this.data.Games.All()
                .Where(g => g.GameState == GameState.WaitingForOpponent)
                .OrderBy(x => x.Name)
            .ThenBy(x => x.DateCreated)
            .ThenBy(x => x.RedPlayer.UserName);
           // .Skip(page * gamesPerPage)
           //     .Take(gamesPerPage);
            return freeToJoinGames;
        }

        [HttpGet]
        public IHttpActionResult AllFreeToJoinGames()
        {
            return AllFreeToJoinGames(0);
        }


        [HttpGet]
        public IHttpActionResult AllFreeToJoinGames(int page)
        {
           
            var gamesPerPage = 2;

          var freeToJoinGames =
              GetAllFreeToJoinGames()
            .Skip(page * gamesPerPage)
                .Take(gamesPerPage);

            var outputGames = new HashSet<GameOutput>();

            foreach (var game in freeToJoinGames)
            {
                var outputGame = new GameOutput
                {
                    Id = game.Id,
                    Name = game.Name,
                    Red = game.RedPlayer.UserName,
                    Blue = "No blue player yet",
                    GameState = game.GameState,
                    DateCreated = game.DateCreated,
                };
                outputGames.Add(outputGame);
            }

            return Ok(outputGames);
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult AllAuthenticatedUserGames(int page)
        {
            var gamesPerPage = 10;
            var outputGames = new HashSet<GameOutput>();
            var authenticatedRedUserGames = this.data.Games.All().Where(g => g.RedPlayer.Id == this.User.Identity.GetUserId());
            var authenticatedBlueUserGames = this.data.Games.All().Where(g => g.RedPlayer.Id == this.User.Identity.GetUserId());
            var allGames = new HashSet<Game>(GetAllFreeToJoinGames());
            foreach (var game in authenticatedRedUserGames)
            {
                allGames.Add(game);
            }
            foreach (var game in authenticatedBlueUserGames)
            {
                allGames.Add(game);
            }

            allGames.OrderBy(x => x.Name)
                .ThenBy(x => x.DateCreated)
                .ThenBy(x => x.RedPlayer.UserName)
                .Skip(page * gamesPerPage)
             .Take(gamesPerPage);

            foreach (var game in allGames)
            {
                var outputGame = new GameOutput
                {
                    Id = game.Id,
                    Name = game.Name,
                    Red = game.RedPlayer.UserName,
                    Blue = "No blue player yet",
                    GameState = game.GameState,
                    DateCreated = game.DateCreated,
                };
                outputGames.Add(outputGame);
            }

            return Ok(outputGames);
        }

        [HttpGet]
        [Authorize]

        public IHttpActionResult GetGameDetails(int id)
        {
            var game = this.data.Games.Find(id);
            var userId = this.User.Identity.GetUserId();
            var yourGuesses = new HashSet<Guess>();
            var opponentGuesses = new HashSet<Guess>();

            if (userId == game.RedPlayerId || userId == game.BluePlayerId)
            {
                var allGameGuesses = this.data.Guesses.All().Where(x => x.GameId == game.Id);

                foreach (var guess in allGameGuesses)
                {
                    if (guess.UserId == userId)
                    {
                        yourGuesses.Add(guess);
                    }
                    else
                    {
                        opponentGuesses.Add(guess);
                    }
                }
                var outputGameDetails = new GameDetailsOutput 
                {
                     Blue = game.BluePlayer.UserName,
                      DateCreated = game.DateCreated,
                       GameState = game.GameState.ToString(),
                        Id = game.Id,
                         Name = game.Name,
                          OpponentGuesses = opponentGuesses.AsQueryable().Select(GuessOutput.FromGuess),
                           Red = game.RedPlayer.UserName,
                            YourColor = userId == game.RedPlayerId ? "Red" : "Blue",
                     YourGuesses = yourGuesses.AsQueryable().Select(GuessOutput.FromGuess),
                     YourNumber = userId == game.RedPlayerId ?game.RedPlayerNumber : game.BluePlayerNumber,
                };

                return Ok(outputGameDetails);
            }
            else
            {
               return BadRequest("You are not allowed to see details for this game");
            }
        }
        
        [HttpPost]
        [Authorize]
        public IHttpActionResult Create(InputGame inputGame)
        {
            var playerId = this.User.Identity.GetUserId();
            var playerName = this.User.Identity.Name;
            var newGame = new Game
            {
                Name = inputGame.Name,
                DateCreated = DateTime.Now,
                RedPlayerId = playerId,
                RedPlayerNumber = inputGame.Number,

            };
            this.data.Games.Add(newGame);
            this.data.SaveChanges();

            var outputGame = new GameOutput
            {
                Id = newGame.Id,
                Name = newGame.Name,
                Blue = "No blue player yet",
                Red = playerName,
                GameState = newGame.GameState,
                DateCreated = newGame.DateCreated,
            };

            return Ok(outputGame);
        }


        [HttpPut]
        [Authorize]

        public IHttpActionResult Join(int id, UserNumber number)
        {
            var gameToJoin = this.data.Games.Find(id);
            var currentPlayerId = this.User.Identity.GetUserId();
            var currentPlayerName = this.User.Identity.Name;

            if (currentPlayerId == gameToJoin.RedPlayerId)
            {
                return BadRequest("You cannot join your own game!");

            }
            if (gameToJoin.GameState != GameState.WaitingForOpponent)
            {
                return BadRequest("This game is not available for join. Please try another game! Thank you");
            }
            else
            {
                gameToJoin.BluePlayerId = currentPlayerId;
                gameToJoin.BluePlayerNumber = number.Number;
                gameToJoin.GameState = GameState.BlueInTurn;
                string message = String.Format(" {0} joined your game {1}. ", currentPlayerName, gameToJoin.Name);          
                                
                var newMessage = new Message
                {
                     GameId = gameToJoin.Id,
                      MessageContent = message,
                       State = MessageState.Unread,
                        Type = MessageType.GameJoined,
                         DateCreated = DateTime.Now,
                          UserId = gameToJoin.RedPlayerId
                };

                this.data.Messages.Add(newMessage);


                this.data.SaveChanges();
                var joinResult = new JoinResult { Result = "You joined game \"The empire strikes back!\"" };
                return Ok(joinResult);

            }


        }
    }
}
