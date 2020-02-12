using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Google.Apis.Plus.v1.Data;
using LinqToTwitter;
using Newtonsoft.Json;
using Roadtrip.Models.ViewModels; 

namespace Roadtrip.Controllers
{
    public class CreateRouteController : Controller
    {
        // GET: CreateRoute
        
        public ActionResult Route()
        {
             
            return View();
        }
       /* public ActionResult Route(string name)
        {
            ViewBag.name = name;
            string[] words = name.Split(' '); 

            return View(); 
        }*/

        public ActionResult DisplayInfo()
        {
            string request = Request.QueryString["Info"];

            MapInfoViewModel myInfo = new MapInfoViewModel()
            {
                Place = request
            };
             
            

            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(myInfo),
                ContentType = "application/json",
                ContentEncoding = System.Text.Encoding.UTF8
            };
        }
    }

    
}