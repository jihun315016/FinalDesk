using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DESK_WEB.Controllers
{
    public class WorkController : Controller
    {
        // GET: Work
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Performance()
        {
            return View();
        }

        public ActionResult InoperativeEquipment()
        {
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
            return View();
        }
    }
}