namespace BullsAndCows.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

    public class Game
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        [ForeignKey("RedPlayer")]
        public string RedPlayerId { get; set; }

        public virtual ApplicationUser RedPlayer { get; set; }

        [ForeignKey("BluePlayer")]
        public string BluePlayerId { get; set; }

        public virtual ApplicationUser BluePlayer { get; set; }

        public GameState GameState { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
