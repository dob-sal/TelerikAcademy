using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullAndCows.Models
{
    public class Game
    {
        private ICollection<Guess> guesses;

        private ICollection<Notification> notifications;

        public Game()
        {
            this.Guesses = new HashSet<Guess>();
            this.Notifications = new HashSet<Notification>();
            this.GameState = GameState.WaitingForOpponent;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public string RedPlayerID { get; set; }

        public virtual ApplicationUser RedPlayer { get; set; }

        public string BluePlayerID { get; set; }

        public virtual ApplicationUser BluePlayer { get; set; }

        public GameState GameState { get; set; }

        public DateTime DateCreated { get; set; }

        public int RedNumber { get; set; }

        public int BlueNumber { get; set; }


        public virtual ICollection<Guess> Guesses
        {
            get { return guesses; }
            set { guesses = value; }
        }

        public virtual ICollection<Notification> Notifications
        {
            get { return notifications; }
            set { notifications = value; }
        }
        
    }
}
