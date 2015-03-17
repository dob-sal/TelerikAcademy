namespace Articles.Models
{
    using System;
    using System.Collections.Generic;

    public class Game
    {
    //"Id": 6,
        public int ID { get; set; }

    //"Name": "The Empire strikes back!",
        public string Name { get; set; }

    //"Blue": "No blue player yet",
        public string BluePlayerID { get; set; }
        public ApplicationUser BluePlayer { get; set; }
        public string BlueNumber { get; set; }

    //"Red": "dodo@minkov.it",  <-this is always the creator
        public string RedPlayerID { get; set; }
        public ApplicationUser RedPlayer { get; set; }
        public string RedNumber { get; set; }

    //"GameState": "WaitingForOpponent",
        public GameState State { get; set; }

    //"DateCreated": "2014-09-23T06:41:51.5816277+03:00"
        public DateTime DateCreated { get; set; }

        public ICollection<Guess> Guesses { get; set; }


    }
}
