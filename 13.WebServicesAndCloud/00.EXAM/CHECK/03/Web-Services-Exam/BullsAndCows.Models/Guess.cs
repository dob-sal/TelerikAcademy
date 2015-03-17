using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BullsAndCows.Models
{
    public class Guess
    {
        public int Id { get; set; }

        //[ForeignKey("User")]
        //public int User_Id { get; set; }

        public virtual User User { get; set; }

        public string UserId { get; set; }

        [ForeignKey("Game")]
        public int Game_Id { get; set; }

        public virtual Game Game { get; set; }

        public string Number { get; set; }

        public DateTime DateMade { get; set; }

        public int CowsCount { get; set; }

        public int BullsCount { get; set; }
    }
}