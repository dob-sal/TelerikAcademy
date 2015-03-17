namespace Articles.WebAPI.Controllers
{
    using Articles.Data;
    using System.Web.Http;

    public class BaseApiController : ApiController
    {
        protected IArticlesData data;

        public BaseApiController(IArticlesData data)
        {
            this.data = data;
        }
    }
}