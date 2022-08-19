using DESK_WEB.Models;
using DESK_WEB.Models.DTO;
using MvcPaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace DESK_WEB.Controllers
{
    public class WorkController : Controller
    {
        // localURL은 임시로 사용하는 url
        string baseUrl = ConfigurationManager.AppSettings["localURL"];
        int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);

        // GET: Work
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Performance()
        {
            return View();
        }

        /// <summary>
        /// Author : 강지훈
        /// 설비 비가동 내역 조회 웹 api
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="keyword"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult InoperativeEquipment(string startDate = "", string endDate = "", string keyword = "", int page = 0)
        {
            if (Session["user"] == null)
                return Redirect($"{baseUrl}Main/Index?IsNotLogin=true");

            if (string.IsNullOrWhiteSpace(startDate) || string.IsNullOrWhiteSpace(endDate))
            {
                startDate = DateTime.Now.AddMonths(-1).ToShortDateString();
                endDate = DateTime.Now.ToShortDateString();
            }
            int curIndex = page > 0 ? page - 1 : 0;

            ViewBag.startDate = startDate;
            ViewBag.endDate = endDate;
            ViewBag.keyword = keyword;

            string url = $"{baseUrl}api/Work/inoperative?startDate={startDate}&endDate={endDate}&keyword={keyword}";
            HttpClient client = new HttpClient();
            HttpResponseMessage resMsg = client.GetAsync(url).Result;
            if (resMsg.IsSuccessStatusCode)
            {
                string resStr = resMsg.Content.ReadAsStringAsync().Result;
                ResMessage<List<InoperativeEquipmentVO>> res = JsonConvert.DeserializeObject<ResMessage<List<InoperativeEquipmentVO>>>(resStr);
                var list = res.Data.ToPagedList(curIndex, pageSize);                
                return View(list);
            }
            return View();
        }

        public ActionResult InspectHistory()
        {
            return View();
        }

        public ActionResult DeffectHistory()
        {
            return View();
        }

        public ActionResult MaterialHistory()
        {
            return View();
        }

        public ActionResult Stats()
        {
            ViewBag.Url = baseUrl;
            return View();
        }
    }
}