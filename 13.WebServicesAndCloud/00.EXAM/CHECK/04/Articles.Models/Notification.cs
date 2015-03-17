namespace Articles.Models
{
    using System;

    public class Notification
    {
    //"Id": 1,
        public int ID { get; set; }

        public int UserID { get; set; }

    //"Message": "dodo@minkov.it joined your game \"New game by doncho@minkov.it\"",
        public string Message { get; set; }

    //"DateCreated": "2014-09-22T12:27:44.77",
        public DateTime DateCreated { get; set; }

    //"Type": "GameJoined",
        public NotificationType Type { get; set; }

    //"State": "Unread",
        public bool Read { get; set; } //Unread is Fasle
    
    //"GameId": 3
        public int GameID;

    }
}
