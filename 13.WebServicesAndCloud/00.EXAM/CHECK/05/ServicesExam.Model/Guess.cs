using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesExam.Model
{
    public class Guess
    {
        public int Id { get; set; }

       // [Required]
        public string UserId { get; set; }

        public string UserName { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int GameId { get; set; }

        public virtual Game Game { get; set; }

       // [Required]
        public string Number { get; set; }

        public DateTime DateMade { get; set; }

        //[Required]
        public int CowsCount { get; set; }

        //[Required]
        public int BullsCount { get; set; }
    }
}
