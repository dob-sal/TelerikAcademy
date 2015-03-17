using Microsoft.Owin;
using Application.WebServices;
using Owin;
using System.Web.Http;
using Ninject.Web.Common.OwinHost;
using Ninject;
using Ninject.Web.WebApi;
using Ninject.Web.WebApi.OwinHost;
using Application.Data;
using System.Reflection;
using Application.Data.Contracts;

[assembly: OwinStartup(typeof(Startup))]

namespace Application.WebServices
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
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
            kernel.Bind<IApplicationData>().To<ApplicationData>().WithConstructorArgument("context", c => new DbContext());
        }
    }
}