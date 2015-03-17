namespace BullsAndCows.Models
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

        public string Name { get; set; }

        public string Number { get; set; }

        public string BlueNumber { get; set; }

        public GameState State { get; set; }

        public string BlueId { get; set; }

        public virtual User Blue { get; set; }

        public string RedId { get; set; }

        public virtual User Red { get; set; }

        public DataType DateCreated { get; set; }
    }
}