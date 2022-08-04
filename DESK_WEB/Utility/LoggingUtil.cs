using DESK_WEB.Models.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DESK_WEB.Utility
{
    public class LoggingUtil
    {
        static string baseUrl = ConfigurationManager.AppSettings["apiURL"];

        /// <summary>
        /// Author : 강지훈
        /// Web api를 통한 에러 로깅
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
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
    }
}
