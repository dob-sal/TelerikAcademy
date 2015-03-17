namespace BullsAndCows.Web.Controllers
{
    using BullsAndCows.Data;

    using System;
    using System.Web;
    using System.Web.Http;

    public class BaseApiController : ApiController
    {
        protected IBullsAndCowsData data;
        
        public BaseApiController()
        {

        }

        public BaseApiController(IBullsAndCowsData data)
        {
            this.data = data;
        }
    }
}