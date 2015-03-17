namespace BullsAndCows.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Game
    {
        public Game()
        {
            this.State = GameState.WaitingForOpponent;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        public string RedPlayerId { get; set; }

        public virtual User RedPlayer { get; set; }

        public string BluePlayerId { get; set; }

        public virtual User BluePlayer { get; set; }

        public GameState State { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
