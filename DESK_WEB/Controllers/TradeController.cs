﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DESK_WEB.Controllers
{
    public class TradeController : Controller
    {
        // GET: Trade
        public ActionResult Index()
        {
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