namespace BullsAndCows.Web.ResponseModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using BullsAndCows.Model;
    using Colors;
    public class GameByIdResponseModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Red { get; set; }

        public string Blue { get; set; }

        public DateTime DateCreated { get; set; }

        public string YourNumber { get; set; }

        public IEnumerable<GuessResponseModel> YourGuesses { get; set; }
        public IEnumerable<GuessResponseModel> OpponentGuesses { get; set; }

        public GameState GameState { get; set; }

        public Color YourColor { get; set; }
    }
}