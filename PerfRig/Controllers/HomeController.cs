using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfRig.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult LoadTest()
        {
            ViewBag.Title = "LoadTest";

            return View();
        }

        public ActionResult WebTest()
        {
            ViewBag.Title = "WebTest";

            return View();
        }
    }
}
