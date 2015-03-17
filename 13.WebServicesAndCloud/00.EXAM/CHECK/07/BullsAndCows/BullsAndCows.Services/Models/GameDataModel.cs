using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using BullsAndCows.Models;
using System.ComponentModel.DataAnnotations;

namespace BullsAndCows.Services.Models
{
    public class GameDataModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Blue { get; set; }

        public string Red { get; set; }

        public string GameState { get; set; }

        public DateTime DateCreated { get; set; }
    }
}