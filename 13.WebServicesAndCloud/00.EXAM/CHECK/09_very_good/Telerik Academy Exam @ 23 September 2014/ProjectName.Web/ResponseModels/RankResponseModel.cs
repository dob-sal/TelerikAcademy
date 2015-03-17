namespace BullsAndCows.Web.ResponseModels
{
    using System;
    using System.Linq.Expressions;

    using BullsAndCows.Model;
    
    public class RankResponseModel
    {
        public static Expression<Func<User, RankResponseModel>> FromUser
        {
            get
            {
                return x => new RankResponseModel
                {
                    Username = x.UserName,
                    Rank = 100 * x.Wins + 15 * x.Losses
                };
            }
        }

        public int Rank { get; set; }

        public string Username { get; set; }
    }
}