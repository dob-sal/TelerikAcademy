using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using BullsAndCows.Models;
using System.ComponentModel.DataAnnotations;

namespace BullsAndCows.Services.Models
{
    public class GameInitialModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Number { get; set; }
    }
}