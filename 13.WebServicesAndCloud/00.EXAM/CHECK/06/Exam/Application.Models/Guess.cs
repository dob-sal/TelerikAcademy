namespace Application.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Guess
    {

        public int Id { get; set; }

        [Required]
        public int PlayerId { get; set; }

        public ApplicationUser Player { get; set; }

        public int GameId { get; set; }

        public virtual Game Game { get; set; }

        public int Number { get; set; }

        public DateTime DateMade { get; set; }

        public int CowsCount { get; set; }

        public int BullsCount { get; set; }
    }
}