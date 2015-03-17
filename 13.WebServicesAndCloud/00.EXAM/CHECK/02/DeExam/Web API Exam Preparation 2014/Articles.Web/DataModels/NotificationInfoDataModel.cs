namespace BullsAndCows.Web.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using AutoMapper;

    using BullsAndCows.Models;

    public class NotificationInfoDataModel
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime DateCreated { get; set; }

        public int GameId { get; set; }

        public NotificationType Type { get; set; }

        public NotificationState State { get; set; }

        // from Niki and Kenov's TicTacToe:
        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Notification, NotificationInfoDataModel>().
                   ForMember(m => m.Id, opt => opt.MapFrom(notification => notification.Id)).
                   ForMember(m => m.Message, opt => opt.MapFrom(notification => notification.Message)).
                   ForMember(m => m.DateCreated, opt => opt.MapFrom(notification => notification.DateCreated)).
                   ForMember(m => m.Type, opt => opt.MapFrom(notification => notification.Type.ToString())).
                   ForMember(m => m.GameId, opt => opt.MapFrom(notification => notification.GameId)).
                   ForMember(m => m.State, opt => opt.MapFrom(notification => notification.State.ToString()));
        }
    }
}