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

        
        /// <summary>
        /// Author : 강지훈
        /// 웹 메인 화면
        /// </summary>
        /// <param name="isLoginFail">로그인 실패 여부</param>
        /// <returns></returns>
        public ActionResult Index(bool? isLoginFail)
        {
            ViewBag.isLoginFail = isLoginFail.HasValue ? isLoginFail.Value : false;
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
                ResMessage<UserVO> res = JsonConvert.DeserializeObject<ResMessage<UserVO>>(resStr);

                if (res.ErrCode == 0)
                {
                    Session["user"] = res.Data;
                    return Redirect("index");
                }
                else
                {
                    return Redirect($"{baseUrl}Main/Index?isLoginFail=true");
                }
            }
            return View();
        }

        public ActionResult UserInfo()
        {
            // 로그인이 되지 않은 경우
            if (Session["user"] == null)
            {
                return PartialView();
            }
            // 로그인이 된 경우
            else
            {
                UserVO user = Session["user"] as UserVO;
                if (user != null)
                    return PartialView(user);
                else
                    return PartialView();
            }
        }

        public ActionResult Login()
        {
            ViewBag.Url = baseUrl;

            // 로그인이 된 않은 경우
            if (Session["user"] != null)
                ViewBag.user = Session["user"] as UserVO;
            
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