using ServicesExam.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesExam.Services.Models
{
    public class GameDetailsOutput
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Blue { get; set; }

        public String Red { get; set; }

        

        public DateTime DateCreated { get; set; }

        public string YourNumber { get; set; }

        public IEnumerable<GuessOutput> YourGuesses {get; set;}

        public IEnumerable<GuessOutput> OpponentGuesses { get; set; }

        public string YourColor { get; set; }

        public string GameState { get; set; }
    }
}