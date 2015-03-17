using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.WebServices.Models
{
    public class GameIdModel
    {
        public GameIdModel()
        {
            this.YourGuesses = new HashSet<Guess>();
            this.OpponentGuesses = new HashSet<Guess>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateCreated { get; set; }

        public string Red { get; set; }

        public string Blue { get; set; }

        public int YourNumber { get; set; }

        public ICollection<Guess> YourGuesses { get; set; }

        public ICollection<Guess> OpponentGuesses { get; set; }

        public string Color { get; set; }
    }
}