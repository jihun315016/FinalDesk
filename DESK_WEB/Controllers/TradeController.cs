using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace DESK_WEB.Controllers
{
    public class TradeController : Controller
    {
        static string baseUrl = ConfigurationManager.AppSettings["apiURL"];

        // GET: Trade
        public ActionResult Index()
        {
            string url = $"{baseUrl}api/Trade/Purchase";
            HttpClient client = new HttpClient();
            HttpResponseMessage resMsg = client.GetAsync(url).Result;
            return View();
        }

        public ActionResult Purchase()
        {
            return View();
        }

        public ActionResult Order()
        {
            return View();
        }
    }
}