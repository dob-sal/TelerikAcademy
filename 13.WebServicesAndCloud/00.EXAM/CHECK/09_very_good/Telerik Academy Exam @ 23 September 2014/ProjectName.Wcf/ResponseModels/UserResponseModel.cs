namespace BullsAndCows.Wcf.ResponseModels
{
    using System;
    using System.Linq.Expressions;

    using BullsAndCows.Model;
    public class UserResponseModel
    {


        public static Expression<Func<User, UserResponseModel>> FromUser
        {
            get
            {
                return x => new UserResponseModel
                {
                    Username = x.UserName,
                    Id = x.Id
                };
            }
        }

        public string Id { get; set; }

        public string Username { get; set; }
    }
}