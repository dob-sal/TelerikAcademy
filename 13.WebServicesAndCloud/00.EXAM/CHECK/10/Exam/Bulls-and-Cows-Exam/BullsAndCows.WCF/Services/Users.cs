namespace BullsAndCows.WCF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using System.Text;
    using System.Web.Http;
    using System.Web.Http.Results;

    using BullsAndCows.Data;
    using BullsAndCows.Models;

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Users" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Users.svc or Users.svc.cs at the Solution Explorer and start debugging.
    public class Users : IUsers
    {
        private IBullsAndCowsData data;

        public Users()
        {
            this.data = new BullsAndCowsData(BullsAndCowsDbContext.Create());
        }

        public ICollection<ApplicationUser> AllUsers()
        {
            return this.data.Users.All().ToList();
        }

        public ApplicationUser UserInfo(string id)
        {
            return this.data.Users.All().First(u => u.Id == id);
        }
    }
}
