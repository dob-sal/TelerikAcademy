namespace BullsAndCows.Web.DataModels
{
    using System;

    using BullsAndCows.Models;

    public class GameInfoDataModel
    {
        public Guid Id { get; set; }

        public string Board { get; set; }

        public string BlueName { get; set; }

        public string RedName { get; set; }

        public GameState State { get; set; }
    }
}