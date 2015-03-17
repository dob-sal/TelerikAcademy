namespace ServicesExam.Services.Controllers
{
    using ServicesExam.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class BaseController : ApiController
    {
        protected IServicesExamData data;
        public BaseController(IServicesExamData data)
        {
            this.data = data;
        }
    }
}
