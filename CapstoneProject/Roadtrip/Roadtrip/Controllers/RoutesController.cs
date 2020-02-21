using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Diagnostics;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        public JsonResult GetEstablishment()
        {
            string key = System.Web.Configuration.WebConfigurationManager.AppSettings["YelpKey"];
            string uri = "https://api.yelp.com/v3/businesses/search?location=97361&limit=20";
            string data = SendRequest(uri, key);

            JObject test = JObject.Parse(data);
            List<string> names = new List<string>();
            List<double> index = new List<double>();
            List<double> ratings = new List<double>();
            List<decimal> longi = new List<decimal>();
            List<decimal> lati = new List<decimal>();
            List<string> BusinessID = new List<string>();

            for (int i = 0; i < 20; i++)
            {

                index.Add(i);
                ratings.Add((double)test["businesses"][i]["rating"]);
                names.Add(((string)test["businesses"][i]["name"]).ToString());
                lati.Add((decimal)test["businesses"][i]["coordinates"]["latitude"]);
                longi.Add((decimal)test["businesses"][i]["coordinates"]["longitude"]);
                BusinessID.Add((string)test["businesses"][i]["id"]);
            }

            var FinalList = new
            {
                name = names,
                rating = ratings,
                indexs = index,
                latitude = lati,
                longitude = longi,
                id = BusinessID
            };

            return Json(FinalList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDetails()
        {
            string ID = Request.QueryString["id"];
            string key = System.Web.Configuration.WebConfigurationManager.AppSettings["YelpKey"];
            string uri = "https://api.yelp.com/v3/businesses/" + ID;
            string data = SendRequest(uri, key);

            JObject test = JObject.Parse(data);
            List<string> name = new List<string>();
            List<double> rating = new List<double>();
            List<string> img = new List<string>();



            name.Add((string)test["name"]);
            rating.Add((double)test["rating"]);
            img.Add((string)test["img_url"]);

            var FinalList = new
            {
                names = name,
                ratings = rating,
                image = img
            };

            return Json(FinalList, JsonRequestBehavior.AllowGet);
        }

        private string SendRequest(string uri, string key)
        {
            Debug.WriteLine(uri);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Headers.Add("Authorization", "Bearer " + key);
            request.Accept = "application/json";

            string jsonString = null;

        public ActionResult DisplayInfo(string myInfo, string city)
        {
            string request = Request.QueryString["myInfo"];
            string myCity = Request.QueryString["city"];
            string myState = Request.QueryString["state"];
            string myKey = System.Web.Configuration.WebConfigurationManager.AppSettings["OpenCageKey"];

            /*Parsing and restructuring the place element*/
            string[] words = myInfo.Split(' ');
            string test = words[0];
            for (int i = 1; i <= words.Length - 1; i++)
            {
                test = test + "+" + words[i];
            }
            /*Parsing and restructuring the City*/
            
            string urlPlace = "https://api.opencagedata.com/geocode/v1/json?q="+ test + "+" + myCity +"&key=" + myKey;
            //string urlCity = "https://api.opencagedata.com/geocode/v1/json?q=" + myCity + "&key=3e00b526f7af428a93598818cf2e926d";
           string json = SendRequest(urlPlace, myKey );
            //string jsonCity = SendRequest(urlCity, key); 
            JObject mapInfo = JObject.Parse(json);
           // JObject cityInfo = JObject.Parse(jsonCity);

            //[JSON].results.[0].bounds.northeast.lat
            //[JSON].results.[0].bounds.northeast.lng



            //string lat =  [JSON].results.[0].bounds.northeast.lat;
            //string lon =  [JSON].results.[0].bounds.northeast.lng; 
            string lat = (string)mapInfo.SelectToken("results.[0].bounds.northeast.lat");
            string lon = (string)mapInfo.SelectToken("results.[0].bounds.northeast.lng");

           /* string cityLat = (string)cityInfo.SelectToken("results.[0].bounds.northeast.lat");
            string cityLon = (string)cityInfo.SelectToken("results.[0].bounds.northeast.lng");*/

            MapInfoViewModel updateInfo = new MapInfoViewModel()
            {
                Lat = lat, 
                Lon = lon,
                
            };
           


            return new ContentResult
            {

                Content = JsonConvert.SerializeObject(updateInfo),
                ContentType = "application/json",
                ContentEncoding = System.Text.Encoding.UTF8
            };
        }


        private string SendRequest(string uri, string credentials)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Headers.Add("Authorization", "token " + credentials);
            request.Accept = "application/json";

            string jsonString = null;
            // TODO: You should handle exceptions here
            using (WebResponse response = request.GetResponse())
            {
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                jsonString = reader.ReadToEnd();
                reader.Close();
                stream.Close();
            }
            return jsonString;
        }
    }
}
