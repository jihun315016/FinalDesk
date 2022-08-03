using DESK_DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DESK_MES.Utility
{
    public class LoggingUtil
    {
        static string baseUrl = ConfigurationManager.AppSettings["apiURL"];

        public static bool LoggingError(LoggingMsgVO msg)
        {
            string url = $"{baseUrl}api/log/writeErrLog";
            HttpClient client = new HttpClient();
            HttpResponseMessage resMsg = client.PostAsJsonAsync(url, msg).Result;

            if (resMsg.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public static bool LoggingInfo(string msg)
        {
            string url = $"{baseUrl}api/log/writeInfoLog?errMsg={msg}";
            HttpClient client = new HttpClient();
            HttpResponseMessage resMsg = client.GetAsync(url).Result;

            if (resMsg.IsSuccessStatusCode)
                return true;
            else
                return false;
        }
    }
}
