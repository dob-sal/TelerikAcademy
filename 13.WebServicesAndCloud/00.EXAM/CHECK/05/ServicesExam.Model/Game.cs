using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesExam.Model
{
    public class Game
    {
        private ICollection<Guess> yourGuesses;
        private ICollection<Guess> opponentGuesses;

        public Game()
        {
            GameState = GameState.WaitingForOpponent;
            this.yourGuesses = new HashSet<Guess>();
            this.opponentGuesses = new HashSet<Guess>();
        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public GameState GameState { get; set; }

        public string RedPlayerId { get; set; }

        public virtual ApplicationUser RedPlayer { get; set; }

        public string BluePlayerId { get; set; }

        public virtual ApplicationUser BluePlayer { get; set; }

        public string RedPlayerNumber { get; set; }

        public string BluePlayerNumber { get; set; }

        public DateTime DateCreated { get; set; }

        public virtual ICollection<Guess> YourGuesses
        {
            get { return this.yourGuesses; }
            set { this.yourGuesses = value; }
        }

        public virtual ICollection<Guess> OpponentGuesses 
        {
            get { return this.opponentGuesses; }
            set { this.opponentGuesses = value; }
        }

       
    }
}
