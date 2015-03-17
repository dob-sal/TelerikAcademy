namespace BullsAndCows.Web.Infrastructure
{
    using System.Threading;
    using Microsoft.AspNet.Identity;
    
    public class AspNetUserNameProvider : IUserNameProvider
    {
        public string GetUserName()
        {
            return Thread.CurrentPrincipal.Identity.GetUserName();
        }
    }
}