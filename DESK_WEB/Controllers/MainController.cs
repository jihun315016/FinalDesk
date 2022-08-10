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
        //string baseUrl = ConfigurationManager.AppSettings["apiURL"];
        static string baseUrl = ConfigurationManager.AppSettings["localURL"];

        // GET: Main
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CheckLogin(UserVO user)
        {
            string url = $"{baseUrl}api/Menu/Login";
            HttpClient client = new HttpClient();
            HttpResponseMessage resMsg = client.PostAsJsonAsync(url, user).Result;
            if (resMsg.IsSuccessStatusCode)
            {
                string resStr = resMsg.Content.ReadAsStringAsync().Result;
                ResMessage<bool> res = JsonConvert.DeserializeObject<ResMessage<bool>>(resStr);

                if (res.Data)
                {
                    // 로그인 성공, 리다이렉션
                }
                else
                {
                    // 로그인 실패 처리
                }
            }
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Url = baseUrl;
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