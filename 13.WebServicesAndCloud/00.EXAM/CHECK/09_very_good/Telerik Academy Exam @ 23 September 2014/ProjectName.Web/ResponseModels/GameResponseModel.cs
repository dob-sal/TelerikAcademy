namespace BullsAndCows.Web.ResponseModels
{
    using System;
    using System.Linq.Expressions;

    using Model;

    public class GameResponseModel
    {
        public static Expression<Func<Game, GameResponseModel>> FromGame
        {
            get
            {
                return x => new GameResponseModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Blue = x.BluePlayer.UserName ?? "No blue player yet",
                    Red = x.RedPlayer.UserName,
                    GameState = x.State.ToString(),
                    DateCreated = x.DateOfCreation
                };
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Red { get; set; }

        public string Blue { get; set; }

        public string GameState { get; set; }

        public DateTime DateCreated { get; set; }
    }
}