namespace Articles.WCF
{
    using Articles.Data;
    using Articles.WCF.DataModels;
    using System.Collections.Generic;
    using System.Linq;
    
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AlertsService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AlertsService.svc or AlertsService.svc.cs at the Solution Explorer and start debugging.
    public class AlertsService : IUsersService
    {
        private UserData data;

        public AlertsService()
        {
            this.data = new UserData(ArticlesDbContext.Create());
        }

        public IEnumerable<UserDataModel> GetUsers()
        {
            var users = this.data.Users.All()
                .OrderBy(a => a.UserName)
                .Select(UserDataModel.FromAppUser).ToList();

            return users;
        }
    }
}
