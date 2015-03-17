using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

//added usings
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using System.Web.Http;
using System.Reflection;
using Articles.Data;

[assembly: OwinStartup(typeof(Articles.WebAPI.Startup))]

namespace Articles.WebAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            //add this
            app.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(GlobalConfiguration.Configuration);
        }

        //add this entire method, from the Github site
        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            RegisterMappings(kernel); //this call actually registers the kernel
            return kernel;
        }

        //add this entire method from the checklist bind IData to Data
        private static void RegisterMappings(StandardKernel kernel)
        {
            kernel.Bind<IArticlesData>().To<ArticlesData>().WithConstructorArgument("context", c => new ArticlesDbContext());
        }
    }
}
