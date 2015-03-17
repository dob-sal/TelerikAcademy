using BullsAndCows.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BullsAndCows.Services.Models
{
    public class GameDetailsModel
    {
        public GameDetailsModel(Game game, BullsAndCows.Models.ApplicationUser curentUser)
        {
            this.Id = game.Id;
            this.Name = game.Name;
            this.DateCreated = game.DateCreated;
            this.Red = game.RedUser.UserName;
            this.Blue = game.BlueUser.UserName;
            this.YourNumber = curentUser.Id == game.RedUserId ? game.RedUserNumber : game.BlueUserNumber;
            this.YourGuesses = curentUser.Id == game.RedUserId ? game.RedUserGuesses : game.BlueUserGuesses;
            this.OpponentGuesses = curentUser.Id == game.RedUserId ? game.BlueUserGuesses : game.RedUserGuesses;
            this.YourColor = curentUser.Id == game.RedUserId ? "Red" : "Blue";
            this.GameState = game.State.ToString();

        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public string Red { get; set; }
        public string Blue { get; set; }
        public int YourNumber { get; set; }
        public ICollection<Guess> YourGuesses { get; set; }
        public ICollection<Guess> OpponentGuesses { get; set; }
        public string YourColor { get; set; }
        public string GameState { get; set; }
    }
}