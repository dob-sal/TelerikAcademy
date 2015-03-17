namespace Articles.Models
{
    using System;

    public class Guess
    {
    //"Id": 15,
        public int ID { get; set; }
    //"UserId": "12d10b41-fdd4-4d61-8ad5-980af83263d8",
        public string UserID { get; set; }
    //"Username": "dodo@minkov.it",
        public string UserName { get; set; }
    //"GameId": 6,
        public int GameID { get; set; }
    //"Number": "1234",
        public string Number { get; set; }
    //"DateMade": "2014-09-23T06:52:47.038633+03:00",
        public DateTime DateMade { get; set; }
    //"CowsCount": 2,
        public int CowsCount { get; set; }
    //"BullsCount": 0
        public int BullsCount { get; set; }
    }
}
