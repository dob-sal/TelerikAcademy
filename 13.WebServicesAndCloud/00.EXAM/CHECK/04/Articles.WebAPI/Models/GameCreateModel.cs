using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Articles.WebAPI.Models
{
    public class GameCreateModel //for use with authorized Create
    {
        public GameCreateModel()
        {

        }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string RedNumber { get; set; }
    }
}