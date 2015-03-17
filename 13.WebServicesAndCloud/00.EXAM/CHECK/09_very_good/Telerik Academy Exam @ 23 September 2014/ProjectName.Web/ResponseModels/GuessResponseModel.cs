namespace BullsAndCows.Web.ResponseModels
{
    using System;
    using System.Linq.Expressions;

    using Model;
    public class GuessResponseModel
    {
        public static Expression<Func<Guess, GuessResponseModel>> FromGuess
        {
            get
            {
                return x => new GuessResponseModel
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    UserName = x.User.UserName,
                    GameId = x.GameId,
                    Number = x.Number,
                    DateMade = x.DateMade,
                    CowsCount = x.CowsCount,
                    BullsCount = x.BullsCount
                };
            }
        }

        public int Id { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public int GameId { get; set; }

        public string Number { get; set; }

        public DateTime DateMade { get; set; }

        public int CowsCount { get; set; }

        public int BullsCount { get; set; }
    }
}