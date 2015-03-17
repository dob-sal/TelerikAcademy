using BullAndCows.Data;
using BullAndCows.Models;
using BullAndCows.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Microsoft.AspNet.Identity;

namespace BullAndCows.WebApi.Controllers
{
    public class GuessController : BaseApiController
    {
         public GuessController()
            : base()
        {
        }

         public GuessController(IBullAndCowsData data)
            : base(data)
        {
        }

         [HttpPost]
         [Authorize]
         public IHttpActionResult MakeGuess(int id, JoinGameModel model)
         {
             var userID = this.User.Identity.GetUserId();

             var newGuess = new Guess
             {
                 UserID = userID,
                 GameId = id,
                 Number = model.Number,
                 DateMade = DateTime.Now,
             };

             this.data.Guesses.Add(newGuess);
             this.data.SaveChanges();

             var newGuessModel = new GuessModel(newGuess);

             return Ok(newGuessModel);

         }
    }
}
