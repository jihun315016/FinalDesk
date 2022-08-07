using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DESK_WEB.Models.DTO
{
    public class LoggingMsgVO
    {
        public string Msg { get; set; }
        public string StackTrace { get; set; }
        public string Source { get; set; }
    }
}