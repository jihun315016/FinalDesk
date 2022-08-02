using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DESK_WEB.Controllers.api
{
    public class LoggingController : ApiController
    {
        public IHttpActionResult WriteLog(string errMsg)
        {
            return Ok();
        }
    }
}
