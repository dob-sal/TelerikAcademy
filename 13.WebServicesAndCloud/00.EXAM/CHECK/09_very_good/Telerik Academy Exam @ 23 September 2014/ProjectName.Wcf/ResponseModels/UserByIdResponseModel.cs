namespace BullsAndCows.Wcf.ResponseModels
{
    using System;
    using System.Linq.Expressions;

    using BullsAndCows.Model;

    public class UserByIdResponseModel
    {
        public static Expression<Func<User, UserByIdResponseModel>> FromUser
        {
            get
            {
                return x => new UserByIdResponseModel
                {
                    Username = x.UserName,
                    Id = x.Id,
                    Losses = x.Losses,
                    Wins = x.Wins,
                    Rank = 100 * x.Wins + 15 * x.Losses
                };
            }
        }

        public string Id { get; set; }

        public int Losses { get; set; }

        public int Rank { get; set; }

        public string Username { get; set; }

        public int Wins { get; set; }
    }
}