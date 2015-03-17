namespace ServicesExam.Services.Models
{
    using ServicesExam.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;

    public class GameOutput
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Blue { get; set; }

        public String Red { get; set; }

        public GameState GameState { get; set; }

        public DateTime DateCreated { get; set; }

        public static Expression<Func<Game, GameOutput>> FromGame
        {
            get
            {
                return game => new GameOutput
                {
                    Id = game.Id,
                    Name = game.Name,
                    Red = game.RedPlayer.UserName,
                    Blue = "No blue player yet",
                    GameState = game.GameState,
                    DateCreated = game.DateCreated,
                };
            }
        }
    }
}