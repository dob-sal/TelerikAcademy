using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Ninject;
using System.Reflection;
using ServicesExam.Data;

[assembly: OwinStartup(typeof(ServicesExam.Services.Startup))]

namespace ServicesExam.Services
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
             RegisterMapping(kernel);
            return kernel;
        }
       
        private static void RegisterMapping(StandardKernel kernel)
        {
            kernel.Bind<IServicesExamData>().To<ServicesExamData>().WithConstructorArgument("context", c => new ApplicationDbContext());
        }


    }
}
