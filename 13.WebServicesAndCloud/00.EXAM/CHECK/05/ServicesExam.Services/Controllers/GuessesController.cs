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
    public class GuessesController : BaseController
    {
        public GuessesController(IServicesExamData data)
            : base(data)
        {

        }


        [HttpPost]
        [Authorize]
        public IHttpActionResult Guess(int id, UserNumber number)
        {
            var currentGame = this.data.Games.Find(id);
            var userId = this.User.Identity.GetUserId();
            var userName = this.User.Identity.Name;
            var guess = new Guess();
            Message message = new Message();
            string contentOfMessage = String.Format("It's your turn in game {0}", currentGame.Name);

            if (currentGame.GameState == GameState.BlueInTurn && userId == currentGame.BluePlayer.Id)
            {
                guess.GameId = currentGame.Id;
                guess.Number = number.Number;
                guess.UserName = userName;
                guess.UserId = userId;
                guess.DateMade = DateTime.Now;
                var cows = GetCowsCount(number.Number, currentGame.RedPlayerNumber);
                var bulls = GetBullsCount(number.Number, currentGame.RedPlayerNumber);
                guess.CowsCount = cows;
                guess.BullsCount = bulls;
                if (bulls == 4)
                {
                    currentGame.GameState = GameState.WonByBlue;
                    var wonMessage = new Message
                      {
                          MessageContent = String.Format("You beat {0} in game \"{1}\"", currentGame.RedPlayer.UserName, currentGame.Name),
                          UserId = currentGame.BluePlayerId,
                          GameId = currentGame.Id,
                          Type = MessageType.GameWon,
                          State = MessageState.Unread,
                          DateCreated = DateTime.Now,

                      };

                    var lostMessage = new Message
                    {
                        MessageContent = String.Format("{0} beat you in game \"{1}\"", currentGame.BluePlayer.UserName, currentGame.Name),
                        UserId = currentGame.RedPlayerId,
                        GameId = currentGame.Id,
                        Type = MessageType.GameLost,
                        State = MessageState.Unread,
                        DateCreated = DateTime.Now,
                    };

                   // currentGame.OpponentGuesses.Add(guess);
                    this.data.Messages.Add(wonMessage);
                    this.data.Messages.Add(lostMessage);
                }
                else
                {
                    message = new Message
                    {
                        MessageContent = contentOfMessage,
                        UserId = currentGame.RedPlayerId,
                        GameId = currentGame.Id,
                        Type = MessageType.YourTurn,
                        State = MessageState.Unread,
                        DateCreated = DateTime.Now,
                            
                    };
                    currentGame.GameState = GameState.RedInTurn;
                    this.data.Messages.Add(message);
                }
               
           

            }
            else if (currentGame.GameState == GameState.RedInTurn && userId == currentGame.RedPlayer.Id)
            {
                guess.GameId = currentGame.Id;
                guess.Number = number.Number;
                guess.UserName = userName;
                guess.UserId = userId;
                guess.DateMade = DateTime.Now;
                var cows = GetCowsCount(number.Number, currentGame.BluePlayerNumber);
                var bulls = GetBullsCount(number.Number, currentGame.BluePlayerNumber);
                guess.CowsCount = cows;
                guess.BullsCount = bulls;
                if (bulls == 4)
                {
                    currentGame.GameState = GameState.WonByRed;
                    
                    var wonMessage = new Message
                    {
                        MessageContent = String.Format("You beat {0} in game \"{1}\"", currentGame.BluePlayer.UserName, currentGame.Name),
                        UserId = currentGame.RedPlayerId,
                        GameId = currentGame.Id,
                        Type = MessageType.GameWon,
                        State = MessageState.Unread,
                        DateCreated = DateTime.Now,

                    };

                    var lostMessage = new Message
                    {
                        MessageContent = String.Format("{0} beat you in game \"{1}\"", currentGame.RedPlayer.UserName, currentGame.Name),
                        UserId = currentGame.BluePlayerId,
                        GameId = currentGame.Id,
                        Type = MessageType.GameLost,
                        State = MessageState.Unread,
                        DateCreated = DateTime.Now,

                    };
                
                    this.data.Messages.Add(wonMessage);
                    this.data.Messages.Add(lostMessage);
                    this.data.SaveChanges();
                }
                else
                {
                    message = new Message
                    {
                        MessageContent = contentOfMessage,
                        UserId = currentGame.BluePlayerId,
                        GameId = currentGame.Id,
                        Type = MessageType.YourTurn,
                        State = MessageState.Unread,
                        DateCreated = DateTime.Now,

                    };
                    currentGame.GameState = GameState.BlueInTurn;
                    this.data.Messages.Add(message);
                }
               
               
               
            }
            else if ((currentGame.GameState == GameState.RedInTurn && this.User == currentGame.BluePlayer) ||
                (currentGame.GameState == GameState.BlueInTurn && this.User == currentGame.RedPlayer))
            {
                return BadRequest("Its your opponent's turn to play!");
            }

            this.data.Guesses.Add(guess);
            this.data.SaveChanges();

            var guessOutput = new GuessOutput
            {
                BullsCount = guess.BullsCount,
                CowsCount = guess.CowsCount,
                DateMade = guess.DateMade,
                GameId = guess.GameId,
                Id = guess.Id,
                Number = guess.Number,
                UserId = guess.UserId,
                UserName = guess.UserName,

            };

            return Ok(guessOutput);

        }

        private int GetBullsCount(string guessNumber, string originalNumber)
        {
            var bulls = 0;
            for (int i = 0; i < guessNumber.Length; i++)
            {
                if (guessNumber[i] == originalNumber[i])
                {
                    bulls++;
                }
            }
            return bulls;
        }

        private int GetCowsCount(string guessNumber, string originalNumber)
        {
            var cows = 0;
            for (int i = 0; i < guessNumber.Length; i++)
            {
                for (int j = 0; j < guessNumber.Length; j++)
                {
                    if (guessNumber[i] == originalNumber[j])
                    {
                        if (i != j)
                        {
                            cows++;
                        }

                    }
                }
            }
            return cows;
        }
    }
}
