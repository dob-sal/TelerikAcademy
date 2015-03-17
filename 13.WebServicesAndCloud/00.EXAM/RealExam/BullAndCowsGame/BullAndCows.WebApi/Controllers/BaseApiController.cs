using BullAndCows.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BullAndCows.WebApi.Controllers
{
    public class BaseApiController : ApiController
    {
        protected IBullAndCowsData data;

        public BaseApiController()
            : this(new BullAndCowsData())
        {
        }
        public BaseApiController(IBullAndCowsData data)
        {
            this.data = data;
        }

    }
}
