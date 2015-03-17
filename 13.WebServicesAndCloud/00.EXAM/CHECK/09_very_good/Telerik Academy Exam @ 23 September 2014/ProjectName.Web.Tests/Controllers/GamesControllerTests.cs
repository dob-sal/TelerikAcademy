using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using BullsAndCows.Data.Ropositories;
using System.Web.Http.Routing;
using System.Web.Http;
using System.Net.Http;
using BullsAndCows.Model;
using System.Linq;
using BullsAndCows.Data;
using BullsAndCows.Web.Controllers;
using System.Threading;
using BullsAndCows.Web.Infrastructure;
using BullsAndCows.GameLogic;
using System.Net;
using BullsAndCows.Web.ResponseModels;

namespace BullsAndCows.Web.Tests.Controllers
{
    [TestClass]
    public class GamesControllerTests
    {
        [TestMethod]
        public void AllGames_WhenGamesInDb_ShouldReturnStatus200AndNonEmptyContent()
        {
            // Arrange
            var repo = Mock.Create<IGenericRepository<Game>>();
            Game[] games = 
            {
                new Game()
                {
                    Name = "Some game"
                }
            };

            Mock.Arrange(() => repo.All())
                .Returns(() => games.AsQueryable());

            var data = Mock.Create<IBullsAndCowsData>();
            Mock.Arrange(() => data.Games)
                .Returns(() => repo);

            // No need to arrange these for the sake of this unit test
            var numberValidator = Mock.Create<IGameNumberValidator>();
            var idProvider = Mock.Create<IUserIdProvider>();
            var nameProvider = Mock.Create<IUserNameProvider>();    
                    
            var controller = new GamesController(data, numberValidator, idProvider, nameProvider);

            this.SetupController(controller);

            // Act
            var response = controller.AllGames().ExecuteAsync(CancellationToken.None).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Content);
        }

        [TestMethod]
        public void AllGames_WhenMoreThan10GamesInDb_ShouldReturnOnly10Games()
        {
            // Arrange
            var repo = Mock.Create<IGenericRepository<Game>>();
            var user = Mock.Create<User>();
            Mock.Arrange(() => user.UserName)
                .Returns(() => "Pesho");

            Game[] games = new Game[12];
            for (int i = 0; i < 12; i++)
            {
                games[i] = new Game()
                {
                    Id = i,
                    State = GameState.WaitingForOpponent,
                    Name = i.ToString(),
                    DateOfCreation = DateTime.Now,
                    RedPlayer = user
                };
            }

            Mock.Arrange(() => repo.All())
                .Returns(() => games.AsQueryable());

            var data = Mock.Create<IBullsAndCowsData>();
            Mock.Arrange(() => data.Games)
                .Returns(() => repo);

            // No need to arrange these for the sake of this unit test
            var numberValidator = Mock.Create<IGameNumberValidator>();
            var idProvider = Mock.Create<IUserIdProvider>();
            var nameProvider = Mock.Create<IUserNameProvider>();

            var controller = new GamesController(data, numberValidator, idProvider, nameProvider);

            this.SetupController(controller);

            // Act
            var response = controller.AllGames().ExecuteAsync(CancellationToken.None).Result;
            var content = response.Content.ReadAsAsync<IQueryable<GameResponseModel>>().Result.ToList();
            // Assert
            Assert.AreEqual(content.Count, 10);
        }

        private void SetupController(ApiController controller)
        {
            string serverUrl = "http://localhost:45435/";

            //Setup the Request object of the controller
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(serverUrl)
            };
            controller.Request = request;

            //Setup the configuration of the controller
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
            controller.Configuration = config;

            //Apply the routes of the controller
            controller.RequestContext.RouteData =
                new HttpRouteData(
                    route: new HttpRoute(),
                    values: new HttpRouteValueDictionary
                    {
                        { "controller", "games" }
                    });
        }
    }
}
