using DESK_WEB.Models;
using DESK_WEB.Models.DTO;
using MvcPaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace DESK_WEB.Controllers
{
    public class TradeController : Controller
    {
        // localURL은 임시로 사용하는 url
        string baseUrl = ConfigurationManager.AppSettings["localURL"];
        int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);

        // GET: Trade
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Purchase(string startDate = "", string endDate = "", string keyword = "",int page = 0)
        {
            if (Session["user"] == null)
                return Redirect($"{baseUrl}Main/Index?IsNotLogin=true");

            ViewBag.Url = baseUrl;

            if (string.IsNullOrWhiteSpace(startDate)|| string.IsNullOrWhiteSpace(endDate))
            {
                startDate = DateTime.Now.AddMonths(-1).ToShortDateString();
                endDate = DateTime.Now.ToShortDateString();
            }
            int curIndex = page > 0 ? page - 1 : 0;

            // https://localhost:44393/api/Trade/Purchase?startDate=2022-07-01&endDate=2022-08-04&keyword=
            string url = $"{baseUrl}api/Trade/Purchase?startDate={startDate}&endDate={endDate}&keyword={keyword}";
            HttpClient client = new HttpClient();
            HttpResponseMessage resMsg = client.GetAsync(url).Result;
            if (resMsg.IsSuccessStatusCode)
            {
                string resStr = resMsg.Content.ReadAsStringAsync().Result;
                ResMessage<List<WebPurchaseVO>> res = JsonConvert.DeserializeObject<ResMessage<List<WebPurchaseVO>>>(resStr);
                var list = res.Data.ToPagedList(curIndex, pageSize);
                return View(list);
            }
            return View();
        }

        public ActionResult Order()
        {
            return View();
        }
    }
}