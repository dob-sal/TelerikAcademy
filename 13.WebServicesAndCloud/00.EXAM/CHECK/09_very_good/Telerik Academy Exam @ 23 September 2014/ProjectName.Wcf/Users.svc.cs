using BullsAndCows.Data;
using BullsAndCows.Wcf.ResponseModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


namespace BullsAndCows.Wcf
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProjectModel1Service" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ProjectModel1Service.svc or ProjectModel1Service.svc.cs at the Solution Explorer and start debugging.
    public class UsersService : Users
    {
        private const int DefaultNumberOfUsersPerPage = 1;

        private IBullsAndCowsData data;

        public UsersService()
        {
            this.data = new BullsAndCowsData(BullsAndCowsDbContext.Create());
        }

        public string GetUsers()
        {
            return this.GetUsersWithPage(0);
        }

        public string GetUsersWithPage(int page)
        {
            var result = this.data.Users.All()
                .OrderBy(u => u.UserName)
                .Select(UserResponseModel.FromUser)
                .Skip(DefaultNumberOfUsersPerPage * page)
                .Take(DefaultNumberOfUsersPerPage);

            return JsonConvert.SerializeObject(result);
        }

        public string GetUserById(string id)
        {
            var user = this.data.Users.All().Select(UserByIdResponseModel.FromUser).FirstOrDefault(u => u.Id == id);

            return JsonConvert.SerializeObject(user);
        }
    }
}
