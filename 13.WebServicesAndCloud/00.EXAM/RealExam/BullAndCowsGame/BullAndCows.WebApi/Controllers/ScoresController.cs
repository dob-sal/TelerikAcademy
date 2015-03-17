using BullAndCows.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BullAndCows.WebApi.Controllers
{
    public class ScoresController : BaseApiController
    {

        public ScoresController()
            : base()
        {
        }

        public ScoresController(IBullAndCowsData data)
            : base(data)
        {
        }

        [HttpGet]
        public IHttpActionResult GetScores()
        {
            var highScores = this.data.Users
                                      .All()
                                      .OrderByDescending(u => u.Rank)
                                      .ThenBy(u => u.UserName)
                                      .Take(10)
                                      .Select(u =>
                                          new
                                          {
                                            UserName = u.UserName,
                                            Rank = u.Rank,
                                          }

                                      );

            return Ok(highScores);
        }


    }
}
