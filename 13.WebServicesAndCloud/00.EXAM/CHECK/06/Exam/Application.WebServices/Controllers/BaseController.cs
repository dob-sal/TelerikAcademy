namespace Application.WebServices.Controllers
{
    using System.Web.Http;

    using Application.Data;
    using Application.Data.Contracts;

    public class BaseController : ApiController
    {
        protected IApplicationData data;

        public BaseController(IApplicationData data)
        {
            this.data = data;
        }

        public BaseController()
            : this(new ApplicationData())
        {
        }
    }
}