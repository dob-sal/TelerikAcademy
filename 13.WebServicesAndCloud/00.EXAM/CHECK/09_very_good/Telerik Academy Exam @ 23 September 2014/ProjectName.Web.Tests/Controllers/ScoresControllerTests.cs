using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BullsAndCows.Web.Tests.Controllers
{
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Web.Controllers;
    using Model;
    using Data;
    using Data.Ropositories;
    using Telerik.JustMock;
    using System.Web.Http.Routing;
    using System.Web.Http;
    using System.Net.Http;
    using System.Threading;
    using System.Net;
    using BullsAndCows.Web.ResponseModels;
    
    [TestClass]
    public class ScoresControllerTests
    {
        [TestMethod]
        public void GetRankings_WhenUsersInDb_ShouldReturnStatus200AndNonEmptyContent()
        {
            // Arrange
            var repo = Mock.Create<IGenericRepository<User>>();
            User[] users = 
            {
                new User()
                {
                    Losses = 3,
                    Wins = 4
                }
            };

            Mock.Arrange(() => repo.All())
                .Returns(() => users.AsQueryable());

            var data = Mock.Create<IBullsAndCowsData>();
            Mock.Arrange(() => data.Users)
                .Returns(() => repo);

            var controller = new ScoresController(data);

            this.SetupController(controller);

            // Act
            var response = controller.GetRankings().ExecuteAsync(CancellationToken.None).Result;
            
            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Content);
        }

        [TestMethod]
        public void GetRankings_WhenMoreThan10UsersInDb_ShouldReturnOnly10Users()
        {
            // Arrange
            var repo = Mock.Create<IGenericRepository<User>>();
            User[] users = new User[12];
            for (int i = 0; i < 12; i++)
            {
                users[i] = new User()
                {
                    Wins = i,
                    Losses = i,
                    UserName = i.ToString()
                };
            }

            Mock.Arrange(() => repo.All())
                .Returns(() => users.AsQueryable());

            var data = Mock.Create<IBullsAndCowsData>();
            Mock.Arrange(() => data.Users)
                .Returns(() => repo);

            var controller = new ScoresController(data);

            this.SetupController(controller);

            // Act
            var response = controller.GetRankings().ExecuteAsync(CancellationToken.None).Result;
            var content = response.Content.ReadAsAsync<IQueryable<RankResponseModel>>().Result.ToArray();
            // Assert
            Assert.AreEqual(content.Count(), 10);
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
                        { "controller", "scores" }
                    });
        }
    }
}
