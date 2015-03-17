using BullsAndCows.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BullsAndCows.WebAPI.Controllers
{
    public class ScoresController : BaseApiController
    {
        public ScoresController(IBullsAndCowsData data)
            : base(data)
        {
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var scores = this.data.Users.All()
                .Select(user => new { user.UserName, user.Rank })
                .OrderByDescending(user => user.Rank)
                .Take(10)
                .ToList();

            return Ok(scores);
        }
    }
}
