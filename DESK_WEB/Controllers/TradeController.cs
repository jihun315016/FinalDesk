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
        // localURL은 임시로 사용하는 url
        static string baseUrl = ConfigurationManager.AppSettings["localURL"];

        // GET: Trade
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Purchase()
        {
            ViewBag.Url = baseUrl;
            return View();
        }

        public ActionResult Order()
        {
            return View();
        }
    }
}