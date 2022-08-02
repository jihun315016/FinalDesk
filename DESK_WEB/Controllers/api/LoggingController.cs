using DESK_DTO;
using DESK_WEB.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DESK_WEB.Controllers.api
{

    [RoutePrefix("api/log")]
    public class LoggingController : ApiController
    {
        LoggingUtility _log;

        public LoggingController()
        {
            _log = new LoggingUtility(DateTime.Now.ToString("yyyyMMdd"), log4net.Core.Level.Info, 30);
        }

        [HttpPost]
        [Route("writeLog")]
        public IHttpActionResult WriteLog(string errMsg)
        {
            _log.WriteError(errMsg);
            return Ok();
        }
    }
}
