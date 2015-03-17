using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class Score
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int ScorePoints { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }
    }
}
