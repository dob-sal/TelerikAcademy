using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullAndCows.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime DateCreated { get; set; }

        public NotificationTypes Type { get; set; }

        public NotificationStates State { get; set; }

        public int GameId { get; set; }

        public virtual Game Game { get; set; }
        public string UserID { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
