using BullAndCows.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace BullAndCows.WebApi.Models
{
    public class GameInfoDataModel
    {


        
        public static Expression<Func<Game, GameInfoDataModel>> FromGame
        {
            get
            {
                return g => new GameInfoDataModel
                {
                    Id = g.Id,
                    Name = g.Name,
                    Blue = g.BluePlayer.UserName,
                    Number = g.RedNumber,
                    Red = g.RedPlayer.UserName,
                    RedPlayerId = g.RedPlayerID,
                    GameState = g.GameState,
                    DateCreated = g.DateCreated

                };
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Blue { get; set; }

        public int Number { get; set; }

        public string Red { get; set; }

        public string RedPlayerId { get; set; }

        public GameState GameState { get; set; }

        public DateTime DateCreated { get; set; }


    }
}