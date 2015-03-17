namespace Application.WebServices.Models
{
    using Application.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    public class GameModel
    {
        public static Expression<Func<Game, GameModel>> FromGame
        {
            get
            {
                return a => new GameModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    RedPlayerId = a.RedPlayerId,
                    BluePlayerId = a.BluePlayerId,
                    GameState = a.GameState,
                    DateCreated = a.DateCreated
                };
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string RedPlayerId { get; set; }

        public string BluePlayerId { get; set; }

        public GameState GameState { get; set; }

        public DateTime DateCreated { get; set; }
    }
}