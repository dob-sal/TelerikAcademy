using Articles.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Articles.WebAPI.Models
{
    public class GameDataModel // for use with unauthorised Gets
    {
        public GameDataModel()
        {
            
        }

        public static Expression<Func<Game, GameDataModel>> FromGame
        {
            get
            {
                return game => new GameDataModel
                {
                    ID = game.ID,
                    Name = game.Name,
                    BlueName = game.BluePlayer.UserName == null ? "No blue player yet" : game.BluePlayer.UserName,
                    RedName = game.RedPlayer.UserName,
                    State = game.State.ToString(),
                    DateCreated = game.DateCreated,
                };
            }
        }

        //"Id": 5,
        public int ID { get; set; }

        //"Name": "Battle of the titans",
        public string Name { get; set; }

        //"Blue": "No blue player yet",
        public string BlueName { get; set; }

        //"Red": "doncho@minkov.it",
        public string RedName { get; set; }

        //"GameState": "WaitingForOpponent",
        public string State { get; set; }

        //"DateCreated": "2014-09-22T14:31:51.067"
        public DateTime DateCreated { get; set; }


        

        

    }
}