using ServicesExam.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ServicesExam.Services.Models
{
    public class GuessOutput
    {

        public int Id { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }


        public int GameId { get; set; }

        
        public string Number { get; set; }

        public DateTime DateMade { get; set; }
      
        public int CowsCount { get; set; }

        public int BullsCount { get; set; }

        public static Expression<Func<Guess, GuessOutput>> FromGuess
        {
            get
            {
                return guess => new GuessOutput
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
            }
        }
    }
}