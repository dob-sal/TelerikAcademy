namespace BullsAndCows.Web.Controllers
{
   
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;

    using Data;
    using GameLogic;
    using Web.Infrastructure;
    using RequestModels;
    using BullsAndCows.Model;
    using BullsAndCows.Web.ResponseModels;
    
    public class GuessController : BaseController
    {
        private IGameResultValidator resultValidator;
        private IBullsAndCowsCalculator bullsAndCowsCalculator;
        private IGameNumberValidator numberValidator;
        private IUserIdProvider userIdProvider;
        public GuessController(
            IBullsAndCowsData data,
            IGameResultValidator resultValidator,
            IBullsAndCowsCalculator bullsAndCowsCalculator,
            IGameNumberValidator numberValidator,
            IUserIdProvider userIdProvider)
            :base(data)
        {
            this.resultValidator = resultValidator;
            this.numberValidator = numberValidator;
            this.userIdProvider = userIdProvider;
            this.bullsAndCowsCalculator = bullsAndCowsCalculator;
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult TakeGuess(int id, JoinRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!this.numberValidator.IsValidGameNumber(model.Number))
            {
                return BadRequest("Invalid user number"); 
            }

            var currentGame = this.Database.Games.All().FirstOrDefault(g => g.Id == id);

            if (currentGame == null)
            {
                return BadRequest("Invalid game id!");
            }

            if (currentGame.State != GameState.BlueInTurn && 
                currentGame.State != GameState.RedInTurn)
            {
                return BadRequest("Game is not currently playing!");
            }

            var userId = this.userIdProvider.GetUserId();

            if (currentGame.RedPlayerId != userId &&
                currentGame.BluePlayerId != userId)
            {
                return BadRequest("You cannot take guesses on a game that's not yours!");
            }

            if (userId == currentGame.RedPlayerId && currentGame.State != GameState.RedInTurn 
                ||
                userId == currentGame.BluePlayerId && currentGame.State != GameState.BlueInTurn)
            {
                return BadRequest("It is not your turn!");
            }

            bool isCurrentPlayerBlue = userId == currentGame.BluePlayerId;
            string opponentsNumber;
            string opponentId;
            string opponentName;
            if (isCurrentPlayerBlue)
            {
                opponentsNumber = currentGame.RedPlayerNumber;
                opponentId = currentGame.RedPlayerId;
                opponentName = currentGame.RedPlayer.UserName;
            }
            else 
            {
                opponentsNumber = currentGame.BluePlayerNumber;
                opponentId = currentGame.BluePlayerId;
                opponentName = currentGame.BluePlayer.UserName;
            }

            var resultAfterGuess = this.resultValidator.GetResult(model.Number, opponentsNumber);

            if (resultAfterGuess == GameResult.Won)
            {
                // Update wins/losses
                this.Database.Users.All().First(u => u.Id == userId).Wins++;
                this.Database.Users.All().First(u => u.Id == opponentId).Losses++;

                // Send notifications
                this.Database.Notifications.Add(new Notification()
                    {
                        DateCreated = DateTime.Now,
                        Message = "You beat " + opponentName + " in game \"" + currentGame.Name + "\"",
                        PlayerId = userId,
                        Type = NotificationType.GameWon,
                        State = NotificationState.Unread,
                        GameId = currentGame.Id
                    });
                this.Database.Notifications.Add(new Notification()
                    {
                        DateCreated = DateTime.Now,
                        Message = opponentName + " beat you in game \"" + currentGame.Name + "\"",
                        PlayerId = opponentId,
                        Type = NotificationType.GameLost,
                        State = NotificationState.Unread,
                        GameId = currentGame.Id
                    });

                // Change game state
                currentGame.State = isCurrentPlayerBlue ? GameState.WonByBlue : GameState.WonByRed;

                // Save guess
                var currentGuess = new Guess()
                {
                    BullsCount = 4,
                    CowsCount = 0,
                    DateMade = DateTime.Now,
                    GameId = currentGame.Id,
                    Number = model.Number,
                    UserId = userId
                };

                this.Database.Guesses.Add(currentGuess);

                // Save
                this.Database.SaveChanges();

                // Return
                var result = new GuessResponseModel()
                {
                    BullsCount = currentGuess.BullsCount,
                    CowsCount = currentGuess.CowsCount,
                    DateMade = currentGuess.DateMade,
                    GameId = currentGuess.GameId,
                    Id = currentGuess.Id,
                    Number = currentGuess.Number,
                    UserId = currentGuess.UserId,
                    UserName = this.Database.Users.All().First(u => u.Id == currentGuess.UserId).UserName
                };

                return Ok(result);
            }
            else
            {
                var bullsCount = this.bullsAndCowsCalculator.GetBulls(opponentsNumber, model.Number);
                var cowsCount = this.bullsAndCowsCalculator.GetCows(opponentsNumber, model.Number);

                // Change game state
                currentGame.State = currentGame.State == GameState.BlueInTurn ? GameState.RedInTurn : GameState.BlueInTurn;

                // Send notifications
                this.Database.Notifications.Add(new Notification()
                {
                    DateCreated = DateTime.Now,
                    Message = "It is your turn in game \"" + currentGame.Name + "\"",
                    PlayerId = opponentId,
                    Type = NotificationType.YourTurn,
                    State = NotificationState.Unread,
                    GameId = currentGame.Id
                });

                // Save guess
                var currentGuess = new Guess()
                {
                    BullsCount = bullsCount,
                    CowsCount = cowsCount,
                    DateMade = DateTime.Now,
                    GameId = currentGame.Id,
                    Number = model.Number,
                    UserId = userId
                };

                this.Database.Guesses.Add(currentGuess);

                // Save
                this.Database.SaveChanges();

                // Return
                var result = new GuessResponseModel()
                {
                    BullsCount = currentGuess.BullsCount,
                    CowsCount = currentGuess.CowsCount,
                    DateMade = currentGuess.DateMade,
                    GameId = currentGuess.GameId,
                    Id = currentGuess.Id,
                    Number = currentGuess.Number,
                    UserId = currentGuess.UserId,
                    UserName = this.Database.Users.All().First(u => u.Id == currentGuess.UserId).UserName
                };

                return Ok(result);
            }

        }
    }
}