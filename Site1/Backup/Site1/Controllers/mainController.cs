using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Site1.Controllers
{
    public class mainController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "1";
            return View();
        }
        public ActionResult About()
        {
            ViewBag.ActiveMenu = "2";
            return View();
        }
        public ActionResult Product()
        {
            ViewBag.ActiveMenu = "3";
            return View();
        }

    }
}
