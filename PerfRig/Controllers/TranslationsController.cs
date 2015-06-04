using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfRig.Controllers
{
    public class TranslationsController : Controller
    {
        // GET: Translations
        public ActionResult Index()
        {
            ViewBag.Title = "Translations";

            return View();
        }
    }
}