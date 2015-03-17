using Application.Data;
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
    public class GameController : BaseController
    {
      //  private ApplicationData db = new ApplicationData();
        const int Default_Page = 10;
       // [Authorize]
        public GameController(IApplicationData data)
            : base(data)
        {
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Create(GameModel model)
        {
           // var game = this.data.Games.Find(id);
            string userId = this.User.Identity.GetUserId();

            var games = new Game
            {
                Id = model.Id,
                Name = model.Name,
                DateCreated = model.DateCreated,
                RedPlayerId = model.RedPlayerId,
            };

            this.data.Games.Add(games);
            this.data.SaveChanges();

            return Ok(model);
        }

        //private IQueryable<Game> GetAllOrderedByGuessBluePlayer()
        //{			
        //    return this.data.Games.All()
        //        .OrderBy(a => a.GameState)
        //        .ThenBy(b => b.Name)
        //        .ThenBy(c => c.DateCreated)
        //        .ThenBy(d => d.RedPlayerId)
        //        .ThenBy(e => e.BluePlayerId == null)
        //        .Take(10);
        //}

        private IQueryable<Game> GetAllOrderedByGuessBluePlayer()
        {
            return this.data.Games.All()
                .OrderBy(a => a.GameState)
                .ThenBy(b => b.Name)
                .ThenBy(c => c.DateCreated)
                .ThenBy(d => d.RedPlayerId == null)
                .ThenBy(e => e.BluePlayerId)
                .Take(10);
        }

        public HttpResponseMessage GetAll(int page)
        {
            if (page < 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Game does not exist.");
            }
            if (page <= 10)
            {

                var games = this.data.Games.All().Select(GameModel.FromGame);

                return Request.CreateResponse(HttpStatusCode.OK, games);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public IHttpActionResult GetDetailsById(int id)
        {
           // var game = this.data.Games.Find(id);
            var game = this.data.Games.All().Where(games => games.Id == id).Select(GameModel.FromGame).FirstOrDefault();

            if (game == null)
            {
                return BadRequest("Game does not exist.");
            }
       
            return Ok(game);
        }

        //public IHttpActionResult Get(ApplicationUser user)
        //{
        //    return OK(UserUsingWithAutorization(0, user.Id));
        //}


        //public IHttpActionResult Get()
        //{
        //    return OK(UserUsingWithAutorization(0));
        //} 

        [HttpGet]
        public IHttpActionResult All()
        {
            return this.All(null, 0);
        }

        [HttpGet]
        public IHttpActionResult All(int page)
        {
            return this.All(Default_Page, page);
        }

        [HttpGet]
        public IHttpActionResult All(string notification)
        {
            return this.All(notification, 0);
        }

        [HttpGet]
        public IHttpActionResult All(int page)
        {
        //    var travelToGet = this.db.Travels.All().Where(travels => travels.Id == id).Select(TravelDescriptionModels.FromTravel).FirstOrDefault();
            var games = this.GetAllOrderedByGuessBluePlayer()
                .Where(a => game != null ? a.Id.Equals(Game, StringComparison.InvariantCultureIgnoreCase) : true)
                .Skip(10)
                .Take(10)
                .Select(GameModel.FromGame).ToList();

            return Ok(games);
        }    

        public IHttpActionResult GetById(int id, int userID)
        {
            if(this.User == null)
            {
                 return NotFound();
            }

            string userId = this.User.Identity.GetUserId();
            GameIdModel gameById = GetById(id, userID);
            return Ok(gameById); 
        }

        private Nodification GetCategory(GameModel model)
        {
            var notification = this.data.Notifications.All()
                .FirstOrDefault(c => c.Id == model.Id);

            if (notification == null)
            {
                notification = new Nodification { Id = model.Id };
                this.data.Notifications.Add(notification);
            }
            return notification;
        }
    }
}
