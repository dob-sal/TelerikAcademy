namespace BullsAndCows.Web.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;

    using AutoMapper;
    using Microsoft.AspNet.Identity;

    using BullsAndCows.Models;
    using BullsAndCows.Web.Mapping;

    public class GameInfoDataModel : IMappableFrom<Game>, IHaveCustomMappings
    {
        private const string noPlayerMessage = "No blue player yet";

        public GameInfoDataModel()
        {
            this.Guesses = new HashSet<Guess>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Blue { get; set; }

        public string Red { get; set; }

        public ICollection<Guess> Guesses { get; set; }

        public string GameState { get; set; }

        public DateTime DateCreated { get; set; }

        // from Niki and Kenov's TicTacToe:
        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Game, GameInfoDataModel>().
                   ForMember(m => m.Name, opt => opt.MapFrom(game => game.Name.ToString())).
                   ForMember(m => m.DateCreated, opt => opt.MapFrom(game => game.DateCreated)).
                   ForMember(m => m.Red, opt => opt.MapFrom(game => game.Red.UserName.ToString())).
                   ForMember(m => m.Blue, opt => opt.MapFrom(game => game.Blue == null ? noPlayerMessage : game.Blue.UserName.ToString())).
                   ForMember(m => m.GameState, opt => opt.MapFrom(game => game.State.ToString()));
        }
    }
}