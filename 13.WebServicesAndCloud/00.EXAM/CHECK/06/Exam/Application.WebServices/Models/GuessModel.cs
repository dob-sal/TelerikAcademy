using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Application.WebServices.Models
{
    public class GuessModel
    {
        public static Expression<Func<Guess, GuessModel>> FromGuess
        {
            get
            {
                return a => new GuessModel
                {
                    Id = a.Id,
                    PlayerId = a.PlayerId,
                    GameId = a.GameId,
                    Number = a.Number,
                    DateMade = a.DateMade,
                    CowsCount = a.CowsCount,
                    BullsCount = a.BullsCount   
                };
            }
        }

        public int Id { get; set; }

        public int PlayerId { get; set; }

        public int GameId { get; set; }

        public int Number { get; set; }

        public DateTime DateMade { get; set; }

        public int CowsCount { get; set; }

        public int BullsCount { get; set; }
    }
}