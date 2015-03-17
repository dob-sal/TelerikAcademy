using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BullsAndCows.Models
{
    public class Game
    {
        private ICollection<Guess> redUserGuesses;
        private ICollection<Guess> blueUserGuesses;


        public Game()
        {
            redUserGuesses = new HashSet<Guess>();
            blueUserGuesses = new HashSet<Guess>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public GameState State { get; set; }

        public DateTime DateCreated { get; set; }

        public string RedUserId { get; set; }

        public virtual ApplicationUser RedUser { get; set; }

        public string BlueUserId { get; set; }

        public virtual ApplicationUser BlueUser { get; set; }

        public int RedUserNumber { get; set; }

        public int BlueUserNumber { get; set; }

        public ICollection<Guess> RedUserGuesses
        {
            get { return this.redUserGuesses; }
            set { this.redUserGuesses = value; }

        }

        public ICollection<Guess> BlueUserGuesses
        {
            get { return this.blueUserGuesses; }
            set { this.blueUserGuesses = value; }

        }
    }
}
