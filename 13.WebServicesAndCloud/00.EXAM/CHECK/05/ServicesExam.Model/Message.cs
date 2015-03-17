using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesExam.Model
{
    public class Message
    {
        public int Id { get; set; }

        public string MessageContent { get; set; }
        public DateTime DateCreated { get; set; }

        public MessageType Type { get; set; }

        public MessageState State { get; set; }

        public int GameId {get; set;}

        public virtual Game Game { get; set; }

        public string UserId { get; set; }
    }
}
