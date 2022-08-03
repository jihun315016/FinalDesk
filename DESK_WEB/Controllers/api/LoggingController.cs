using DESK_DTO;
using DESK_WEB.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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

        [HttpGet]
        [Route("writeLog")]
        public IHttpActionResult WriteLog(string errMsg)
        {
            _log.WriteError(errMsg);
            _log.WriteInfo(errMsg);
            using (StreamWriter sw = new StreamWriter(@"C:\Users\GDC9\Documents\test.log", true, Encoding.UTF8))
            {
                sw.WriteLine(System.Environment.CurrentDirectory);
            }
            return Ok();
        }
    }

    class temp
    {
        public string Name { get; set; }
        public string ErrMsg { get; set; }
    }
}
