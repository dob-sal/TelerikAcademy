using BullsAndCows.Data;
using BullsAndCows.WebAPI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BullsAndCows.WebAPI.Controllers
{
    public class GamesController : BaseApiController
    {
        private IUserIdProvider userIdProvider;

        public GamesController(IUserIdProvider userIdProvider, IBullsAndCowsData data)
            :base(data)
        {
            this.userIdProvider = userIdProvider;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            //var games = this.data.Games.All();
            return Ok();
        }
    }
}
