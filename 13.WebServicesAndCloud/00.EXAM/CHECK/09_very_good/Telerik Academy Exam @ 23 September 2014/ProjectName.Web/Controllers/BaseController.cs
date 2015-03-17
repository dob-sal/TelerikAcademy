using BullsAndCows.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace BullsAndCows.Web.Controllers
{
    public class BaseController : ApiController
    {
        private IBullsAndCowsData data;

        protected BaseController(IBullsAndCowsData data)
        {
            this.data = data;
        }

        protected IBullsAndCowsData Database
        {
            get
            {
                return this.data;
            }
        }
    }
}