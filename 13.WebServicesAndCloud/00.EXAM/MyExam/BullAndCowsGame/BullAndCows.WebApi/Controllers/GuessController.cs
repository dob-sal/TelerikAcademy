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
        private BullAndCowsDbContext db = new BullAndCowsDbContext(); 
        
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

         //    var newGuess = new Guess
         //    {
         //        UserID = userID,
         //        GameId = id,
         //        Number = model.Number,
         //        DateMade = DateTime.Now,
         //    };

             var newGuessWithCreate = this.db.Guesses.Create();
             newGuessWithCreate.UserID = userID;
             newGuessWithCreate.GameId = id;
             newGuessWithCreate.Number = model.Number;
             newGuessWithCreate.DateMade = DateTime.Now;

             this.db.Guesses.Add(newGuessWithCreate);
             this.db.SaveChanges();

        //     this.data.Guesses.Add(newGuessWithCreate);
        //     this.data.SaveChanges();



             var newGuessModel = new GuessModel(newGuessWithCreate);

             return Ok(newGuessModel);

         }
    }
}
