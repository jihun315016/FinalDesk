using DESK_WEB.Models;
using DESK_WEB.Models.DTO;
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
    public class MainController : Controller
    {
        string baseUrl = ConfigurationManager.AppSettings["apiURL"];

        // GET: Main
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return PartialView();
        }

        /// <summary>
        /// Author : 강지훈
        /// 메뉴 리스트 동적 할당
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMenuList()
        {
            string url = $"{baseUrl}api/Menu/Menu";

            HttpClient client = new HttpClient();
            HttpResponseMessage resMsg = client.GetAsync(url).Result;
            if (resMsg.IsSuccessStatusCode)
            {
                string resStr = resMsg.Content.ReadAsStringAsync().Result;
                ResMessage<List<MenuVO>> res = JsonConvert.DeserializeObject<ResMessage<List<MenuVO>>>(resStr);
                List<MenuVO> list = res.Data;

                return View(list);
            }

            // null이나 빈 값을 보냈는데 뷰에서 html helper 안 쓰면 오류 남
            return View(new List<MenuVO>());
        }
    }
}