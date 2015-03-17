namespace BullsAndCows.Web.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using AutoMapper;
    using Microsoft.AspNet.Identity;

    using BullsAndCows.Models;

    public class GuessInfoDataModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Username { get; set; }

        public int GameId { get; set; }

        public string Number { get; set; }

        public DateTime DateMade { get; set; }

        public byte CowsCount { get; set; }

        public byte BullsCount { get; set; }


        // from Niki and Kenov's TicTacToe:
        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Guess, GuessInfoDataModel>().
                   ForMember(m => m.Id, opt => opt.MapFrom(guess => guess.Id)).
                   ForMember(m => m.UserId, opt => opt.MapFrom(guess => guess.UserId.ToString())).
                   ForMember(m => m.Username, opt => opt.MapFrom(guess => guess.User.UserName)).
                   ForMember(m => m.DateMade, opt => opt.MapFrom(guess => guess.DateMade)).
                   ForMember(m => m.GameId, opt => opt.MapFrom(guess => guess.GameId)).
                   ForMember(m => m.Number, opt => opt.MapFrom(guess => guess.Number)).
                   ForMember(m => m.CowsCount, opt => opt.MapFrom(guess => guess.CowsCount)).
                   ForMember(m => m.BullsCount, opt => opt.MapFrom(guess => guess.BullsCount));
        }
    }
}