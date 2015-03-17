using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BullsAndCows.Data;

namespace BullsAndCows.Web.Controllers
{
    [Authorize]
    public abstract class BaseApiController : ApiController
    {
        protected IBullsAndCowsData data;

        public BaseApiController()
            : this(new BullsAndCowsData())
        {
        }
        protected BaseApiController(IBullsAndCowsData data)
        {
            this.data = data;
        }
    }
}