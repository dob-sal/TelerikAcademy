using Articles.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Articles.WebAPI.Models
{
    public class GameDetailsModel
    {
        
        
        public GameDetailsModel()
        {
            
        }

        /*
         */

        public static Expression<Func<Game, GameDetailsModel>> FromGame
        {
            get
            {
                return game => new GameDetailsModel
                {
                    ID = game.ID,
                    Name = game.Name,
                    Blue = game.BluePlayer.UserName == null ? "No blue player yet" : game.BluePlayer.UserName,
                    Red = game.RedPlayer.UserName,
                    GameState = game.State.ToString(),
                    DateCreated = game.DateCreated,
                    YourColor = null,
                    YourNumber = null
                };
            }
        }

        //"Id": 5,
        public int ID { get; set; }

        //"Name": "Battle of the titans",
        public string Name { get; set; }

        //"Blue": "No blue player yet",
        public string Blue { get; set; }

        //"Red": "doncho@minkov.it",
        public string Red { get; set; }

        //"GameState": "WaitingForOpponent",
        public string GameState { get; set; }

        //"DateCreated": "2014-09-22T14:31:51.067"
        public DateTime DateCreated { get; set; }

        public string YourColor { get; set; }

        public string YourNumber { get; set; }

        //collections of guesses
    }
}