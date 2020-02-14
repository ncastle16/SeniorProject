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

        public ActionResult DisplayInfo(string myInfo)
        {
            string request = Request.QueryString["myInfo"];
            
            /*Parsing and restructuring the place element*/
            string[] words = myInfo.Split(' ');
            string test = words[0];
            for (int i = 1; i <= words.Length - 1; i++)
            {
                test = test + "+" + words[i];
            }
            /*Parsing and restructuring the City*/

            string myURL = "https://www.google.com/maps/embed/v1/place?key=AIzaSyCEqgGvU1YUzAU4ddBoYrjTwIYx2VTaAEU&q=" + test + "," + "Seattle+WA";

            MapInfoViewModel info = new MapInfoViewModel()
            {
                Place = myInfo,
                URL = myURL

            };

            return new ContentResult
            {

                Content = JsonConvert.SerializeObject(info),
                ContentType = "application/json",
                ContentEncoding = System.Text.Encoding.UTF8
            };
        }

        
    }

    
}