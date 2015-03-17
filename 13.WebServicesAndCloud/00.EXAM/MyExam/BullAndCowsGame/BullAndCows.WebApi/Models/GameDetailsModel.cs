using BullAndCows.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace BullAndCows.WebApi.Models
{
    public class GameDetailsModel
    {

        public GameDetailsModel(Game game)
        {
            this.Id = game.Id;
            this.Name = game.Name;
            this.DateCreated = game.DateCreated;
            this.GameState = game.GameState;

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }

        public string Red { get; set; }

        public string Blue { get; set; }

        public int YourNumber { get; set; }

        public string YourColor { get; set; }

        public GameState GameState { get; set; }

    }
}