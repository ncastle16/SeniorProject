using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Roadtrip.Models.ViewModels; 

namespace Roadtrip.Controllers
{
    public class RoutesController : Controller
    {
        // GET: Routes
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string name)
        {
            /*Splitting the place
            string[] words = name.Split(' ');
            string test = words[0];
            for (int i = 1; i <= words.Length-1; i++)
            {
                test = test + "+"+ words[i]; 
            }
            string myURL = "https://www.google.com/maps/embed/v1/place?key=AIzaSyArb3QIXJppRRSFTRgi8KZ51vrAm8yEExM&q=" + test + "," + "Seattle+WA"; 

            MapInfoViewModel myInfo = new MapInfoViewModel()
            {
                Place = test, 
                URL = myURL
             
            };*/

            return View(); 
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult DisplayInfo(string myInfo, string city)
        {
            string request = Request.QueryString["myInfo"];
            string myCity = Request.QueryString["city"];
            string myState = Request.QueryString["state"]; 

            /*Parsing and restructuring the place element*/
            string[] words = myInfo.Split(' ');
            string test = words[0];
            for (int i = 1; i <= words.Length - 1; i++)
            {
                test = test + "+" + words[i];
            }
            /*Parsing and restructuring the City*/

            string myURL = "https://www.google.com/maps/embed/v1/place?key=AIzaSyCEqgGvU1YUzAU4ddBoYrjTwIYx2VTaAEU&q=" + test + "," + myCity + "+" + myState;

            MapInfoViewModel info = new MapInfoViewModel()
            {
                Place = myInfo,
                URL = myURL, 
                State = myState,
                City = myCity

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