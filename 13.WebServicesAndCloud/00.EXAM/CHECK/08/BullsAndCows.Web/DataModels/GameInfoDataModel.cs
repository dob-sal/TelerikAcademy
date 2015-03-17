namespace BullsAndCows.Web.DataModels
{
    using System;

    using BullsAndCows.Models;

    public class GameInfoDataModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string RedPlayerName { get; set; }

        public string BluePlayerName { get; set; }

        public GameState State { get; set; }

        public DateTime DateCreated { get; set; }
    }
}