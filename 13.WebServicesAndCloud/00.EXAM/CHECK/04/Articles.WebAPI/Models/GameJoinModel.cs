using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Articles.WebAPI.Models
{
    public class GameJoinModel //for use with authorized Create
    {
        public GameJoinModel()
        {

        }

        [Required]
        public string Number { get; set; }
    }
}