using BullAndCows.Data;
using BullAndCows.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Microsoft.AspNet.Identity;
using BullAndCows.Models;

namespace BullAndCows.WebApi.Controllers
{
    public class GamesController : BaseApiController
    {
        private static Random rand = new Random();
        const int defaultPageSize = 10;
        
        public GamesController()
            : base()
        {
        }
        
        public GamesController(IBullAndCowsData data)
            : base(data)
        {
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult CreateGame(GameInfoDataModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var userID = this.User.Identity.GetUserId();

            var newGame = new Game
            {
                Name = model.Name,
                RedNumber = model.Number,
                RedPlayerID = userID,
                DateCreated = DateTime.Now
            
            };

            this.data.Games.Add(newGame);
            this.data.SaveChanges();

            model.Id = newGame.Id;
            model.RedPlayerId = newGame.RedPlayerID;
            model.DateCreated = newGame.DateCreated;
            model.GameState = newGame.GameState;

            return Created("",model);
        }


        [HttpPut]
        [Authorize]
        public IHttpActionResult JoinGame(int id, JoinGameModel model)
        {
            var currentUserID = this.User.Identity.GetUserId();
            var currentGame = this.data.Games.Find(id);
            var currentUserName = this.data.Users.Find(currentUserID).UserName;

            var redPlayerId = currentGame.RedPlayerID;

            if (currentGame.GameState != 0)
            {
                return BadRequest("This game is not waiting for opponent!");
            }

            if (currentGame == null)
            {
                return BadRequest("Such game does not exists!");
            }

            if (currentUserID == redPlayerId)
            {
                return BadRequest("This game was created by you!");
            }

            var turn = rand.Next(1, 3);

            currentGame.BluePlayerID = currentUserID;
            currentGame.BlueNumber = model.Number;
            currentGame.GameState = (GameState)turn;

            var redPlayerGameJoinedNotification = new Notification
            {
                DateCreated = DateTime.Now,
                GameId = currentGame.Id,
                Message = currentUserName + " joined your game " + currentGame.Name,
                Type = NotificationTypes.GameJoined,
                State = NotificationStates.Unread,
                UserID = currentGame.RedPlayerID,
                
            };

            var bluePlayerGameJoinedNotification = new Notification
            {
                DateCreated = DateTime.Now,
                GameId = currentGame.Id,
                Message = "You joined game " + currentGame.Name,
                Type = NotificationTypes.GameJoined,
                State = NotificationStates.Unread,
                UserID = currentUserID,

            };

            var inTurnNotification = new Notification
            {
                DateCreated = DateTime.Now,
                GameId = currentGame.Id,
                Message = "It is your turn in game " + currentGame.Name,
                Type = NotificationTypes.YourTurn,
                State = NotificationStates.Unread,
            };

            if (turn == 1)
            {
                inTurnNotification.UserID = currentGame.RedPlayerID;
            }
            else if (turn == 2)
            {
                inTurnNotification.UserID = currentUserID;
            }

            this.data.Notifications.Add(redPlayerGameJoinedNotification);
            this.data.Notifications.Add(inTurnNotification);

            this.data.SaveChanges();


            return Ok(bluePlayerGameJoinedNotification.Message);
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetGameDetailsById(int id)
        {
            
            var userID = this.User.Identity.GetUserId();

            var game = this.data.Games.Find(id);
            if (game == null)
            {
                return NotFound();
            }

            var articleModel = new GameDetailsModel(game);

            var redPlayerName = game.RedPlayer.UserName;

            var bluePlayerName = "No blue player yet";
            if (game.BluePlayer != null)
            {
                bluePlayerName = game.BluePlayer.UserName;
            }

            articleModel.Red = redPlayerName;
            articleModel.Blue = bluePlayerName;

            if (game.RedPlayerID == userID)
            {
                articleModel.YourColor = "red";
                articleModel.YourNumber = game.RedNumber;
            }
            else if (game.BluePlayerID == userID)
            {
                articleModel.YourColor = "blue";
                articleModel.YourNumber = game.BlueNumber;
            }

            articleModel.GameState = game.GameState;

            return Ok(articleModel);
 
        }


        [HttpGet]
        public IHttpActionResult GetGame()
        {
            return GetGame(0);
        }

        [HttpGet]
        public IHttpActionResult GetGame(int page)
        {
            var games = GetAllSorted()
                .Skip(page * defaultPageSize)
                .Take(defaultPageSize);

            return Ok(games);
        }

        private IEnumerable<GameInfoDataModel> GetAllSorted()
        {
            return this.data.Games.All()
                .OrderBy(g => g.GameState)
                .ThenBy(g => g.Name)
                .ThenBy(g => g.DateCreated)
                .ThenBy(g => g.RedPlayer.UserName)
                .Select(GameInfoDataModel.FromGame);
        }

    }
}
