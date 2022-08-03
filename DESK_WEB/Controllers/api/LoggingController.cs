using DESK_DTO;
using DESK_WEB.Utility;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
namespace DESK_WEB.Controllers.api
{

    [RoutePrefix("api/log")]
    public class LoggingController : ApiController
    {
        ILog log;

        public LoggingController()
        {
            log = LogManager.GetLogger(typeof(LoggingController));
        }

        [HttpGet]
        [Route("writeErrLog")]
        public IHttpActionResult WriteLog(string errMsg)
        {
            log.Error(errMsg);
            return Ok();
        }
    }
}
