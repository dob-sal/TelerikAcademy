namespace BullsAndCows.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Game
    {
        public Game()
        {
            this.State = GameState.AvailableForJoining;
            this.DateCreated = DateTime.Now;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public GameState State { get; set; }

        public DateTime DateCreated { get; set; }

        public string BlueId { get; set; }

        public virtual GameUser Blue { get; set; }

        [Required]
        public string RedId { get; set; }
                
        public virtual GameUser Red { get; set; }
    }
}
