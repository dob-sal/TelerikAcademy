namespace BullsAndCows.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Game
    {
        public Game()
        {
            this.State = GameState.WaitingForOpponent;
        }

        public int Id { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        public DateTime DateOfCreation { get; set; }

        public GameState State { get; set; }

        [Required]
        public string RedPlayerId { get; set; }

        public virtual User RedPlayer { get; set; }

        [Required]
        public string RedPlayerNumber { get; set; }

        public string BluePlayerId { get; set; }

        public virtual User BluePlayer { get; set; }

        public string BluePlayerNumber { get; set; }
    }
}
