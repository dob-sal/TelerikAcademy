using BullAndCows.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BullAndCows.WebApi.Models
{
    public class GuessModel
    {
        public GuessModel()
        {
        }
        
        public GuessModel(Guess guess)
        {
            this.Id = guess.Id;
            this.UserId = guess.UserID;
            this.Number = guess.Number;
            this.GameId = guess.GameId;
            this.DateMade = guess.DateMade;

        }

        public int Id { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public int GameId { get; set; }

        public int Number { get; set; }

        public DateTime DateMade { get; set; }

        public int CowsCount { get; set; }
        public int BullsCount { get; set; }
    }
}