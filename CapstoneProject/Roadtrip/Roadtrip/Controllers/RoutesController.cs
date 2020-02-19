using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Roadtrip.Controllers
{
    public class RoutesController : Controller
    {
        // GET: Routes
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        //Work for google api to get establishment names into a list view

        // delete before pushing noah damn it
        // example url for monmouth long and lat with a aradius. Searches for type: bar
        public JsonResult GetEstablishment()
        {
            string key = "StupidFuckingPlaceHolder";
            string uri = "https://api.yelp.com/v3/businesses/search?location=97361&limit=4";
            string data = SendRequest(uri, key);

            JObject test = JObject.Parse(data);
            List<string> names = new List<string>();
            List<double> index = new List<double>();
            List<double> ratings = new List<double>();
            List<decimal> longi = new List<decimal>();
            List<decimal> lati = new List<decimal>();
            List<string> BusinessID = new List<string>();

            for (int i = 0; i < 4; i++) { 

                index.Add(i);
                ratings.Add((double)test["businesses"][i]["rating"]);
                names.Add(((string)test["businesses"][i]["name"]).ToString());
                lati.Add((decimal)test["businesses"][i]["coordinates"]["latitude"]);
                longi.Add((decimal)test["businesses"][i]["coordinates"]["longitude"]);
                BusinessID.Add((double)["businesses"][i]["id"]);
            }
         


            var FinalList = new
            {
                name = names,
                rating = ratings,
                indexs = index,
                latitude = lati,
                longitude = longi,
                businessID = BusinessID
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
