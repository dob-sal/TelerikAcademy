using Application.Data.Contracts;
using Application.Models;
using Application.WebServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Microsoft.AspNet.Identity;

namespace Application.WebServices.Controllers
{
    public class GuessController : BaseController
    {
        public GuessController(IApplicationData data)
            : base(data)
        {
        }

        //public IHttpActionResult Post(int Id, GuessModel guessModel)
        //{
        //}

        //[HttpGet]
        //public IHttpActionResult All(int id)
        //{
        //    var guesses = this.data.Guesses.Find(id);
        //    if (guesses == null)
        //    {
        //        return NotFound();
        //    }

        //    //var comments = guesses.BullsCount.AsQueryable().Take(10)
        //    //    .Select(CommentDataModel.FromComment);

        //    return Ok(comments);
        //}

        //[HttpPost]
        //public IHttpActionResult Create(int id, CommentDataModel model)
        //{
        //    var newGuess = new Guess
        //    {
        //        PlayerId = this.User.Identity.GetUserId(),
        //        Content = model.Content,
        //        DateCreated = DateTime.Now
        //    };

        //    this.data.Games.Find(id)..Add(newComment);
        //    this.data.SaveChanges();

        //    model.ID = newComment.ID;
        //    model.DateCreated = newComment.DateCreated;

        //    return Ok(model);
        //}
    }
}
        
    

