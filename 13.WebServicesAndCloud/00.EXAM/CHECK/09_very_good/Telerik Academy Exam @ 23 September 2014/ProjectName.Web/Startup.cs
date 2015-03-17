using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using BullsAndCows.Data;
using System.Web.Http;
using System.Reflection;
using BullsAndCows.Web.Infrastructure;
using BullsAndCows.GameLogic;

[assembly: OwinStartup(typeof(BullsAndCows.Web.Startup))]

namespace BullsAndCows.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(GlobalConfiguration.Configuration);

        }
        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            RegisterMappings(kernel);

            return kernel;
        }

        private static void RegisterMappings(StandardKernel kernel)
        {
            kernel.Bind<IBullsAndCowsData>().To<BullsAndCowsData>()
                .WithConstructorArgument("context", c => new BullsAndCowsDbContext());

            kernel.Bind<IGameResultValidator>().To<GameResultValidator>();

            kernel.Bind<IGameNumberValidator>().To<GameNumberValidator>();

            kernel.Bind<IUserIdProvider>().To<AspNetUserIdProvider>();

            kernel.Bind<IUserNameProvider>().To<AspNetUserNameProvider>();
            
            kernel.Bind<IBullsAndCowsCalculator>().To<BullsAndCowsCalculator>();

        }
    }
}
