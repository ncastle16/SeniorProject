using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace Roadtrip.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            string path = Server.MapPath("~/Uploads/");
            if (System.IO.File.Exists(path + User.Identity.Name + ".jpeg"))
            {
                ViewBag.loggedIn = true;
            }
            else
                ViewBag.loggedIn = false;


            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}