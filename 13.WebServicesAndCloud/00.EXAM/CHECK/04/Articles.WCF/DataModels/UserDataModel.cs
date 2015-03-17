using Articles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Articles.WCF.DataModels
{
    public class UserDataModel
    {
        public static Expression<Func<ApplicationUser, UserDataModel>> FromAppUser
        {
            get
            {
                return a => new UserDataModel
                {
                    Name = a.UserName,
                    ID = a.Id
                };
            }
        }

        public string ID { get; set; }

        public string Name { get; set; }
    }
}