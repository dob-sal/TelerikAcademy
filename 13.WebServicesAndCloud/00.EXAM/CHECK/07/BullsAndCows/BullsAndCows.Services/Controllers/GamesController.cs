using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

using Microsoft.AspNet.Identity;
using BullsAndCows.Data;
using BullsAndCows.Models;
using BullsAndCows.Services.Models;
using BullsAndCows.Web.Controllers;

namespace BullsAndCows.Services.Controllers
{
    public class GamesController : ApiController
    {
        private BullsAndCowsDbContext db = new BullsAndCowsDbContext();

        [HttpPost]
        [Authorize]
        public IHttpActionResult Create(GameInitialModel initialModel)
        {
            var currentUserId = this.User.Identity.GetUserId();

            var game = new Game
            {
                Name = initialModel.Name,
                State = GameState.WaitingForOpponent,
                DateCreated = DateTime.Now,
                RedUserId = currentUserId,
                BlueUserId = null,
                RedUserNumber = initialModel.Number,
                BlueUserNumber = 0
            };

            this.db.Games.Add(game);
            this.db.SaveChanges();

            var model = new GameDataModel
            {
                Id = game.Id,
                Name = game.Name,
                Blue = "No blue player yet",
                Red = this.User.Identity.Name,
                GameState = GameState.WaitingForOpponent.ToString(),
                DateCreated = game.DateCreated
            };

            return Ok(model);
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var games = db.Games
                .Where(g => g.State == GameState.WaitingForOpponent)
                .OrderBy(g => g.State.ToString())
                .ThenBy(g => g.Name)
                .ThenBy(g => g.DateCreated)
                .ThenBy(g => g.RedUser.UserName)
                .Take(10)
                .Select(b => new GameDataModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Blue = b.BlueUser.UserName,
                    Red = b.RedUser.UserName,
                    GameState = GameState.WaitingForOpponent.ToString(),
                    DateCreated = b.DateCreated
                }).ToList();

            return Ok(games);
        }

        [HttpGet]
        public IHttpActionResult All(int page)
        {
            var games = db.Games
                .Where(g => g.State == GameState.WaitingForOpponent)
                .OrderBy(g => g.State.ToString())
                .ThenBy(g => g.Name)
                .ThenBy(g => g.DateCreated)
                .ThenBy(g => g.RedUser.UserName)
                .Skip(page * 10)
                .Take(10)
                .Select(b => new GameDataModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Blue = b.BlueUser.UserName,
                    Red = b.RedUser.UserName,
                    GameState = GameState.WaitingForOpponent.ToString(),
                    DateCreated = b.DateCreated
                });

            return Ok(games);
        }

        //[HttpGet]
        //[Authorize]
        //public IHttpActionResult All()
        //{
        //    var games = db.Games
        //        .Where(g => g.RedUser.Id == this.User.Identity.GetUserId() && g.BlueUser.Id == this.User.Identity.GetUserId() && g.State == GameState.WaitingForOpponent)
        //        .OrderBy(g => g.State.ToString())
        //        .ThenBy(g => g.Name)
        //        .ThenBy(g => g.DateCreated)
        //        .ThenBy(g => g.RedUser.UserName)
        //        .Take(10)
        //        .Select(b => new GameDataModel
        //        {
        //            Id = b.Id,
        //            Name = b.Name,
        //            Blue = b.BlueUser.UserName,
        //            Red = b.RedUser.UserName,
        //            GameState = GameState.WaitingForOpponent.ToString(),
        //            DateCreated = b.DateCreated
        //        }).ToList();

        //    return Ok(games);
        //}

        [HttpGet]
        [Authorize]
        public IHttpActionResult Details(int id)
        {
            var game = this.db.Games.Find(id);
            if (game == null)
            {
                return NotFound();
            }

            if (game.State == GameState.WaitingForOpponent)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No game with ID = {0} that satisfies the criteria", id)),
                    ReasonPhrase = "The game has to be currently played - not finished or waiting for opponent"
                };
                throw new HttpResponseException(resp);
            }

            BullsAndCows.Models.ApplicationUser currentUser = this.db.Users.Find(this.User.Identity.GetUserId());

            var gameDetailsModel = new GameDetailsModel(game, currentUser);

            return Ok(gameDetailsModel);
        }

    }
}