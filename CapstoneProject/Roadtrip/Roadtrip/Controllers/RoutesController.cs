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
            List<string> phone = new List<string>();
            List<string> address = new List<string>();
            List<string> city = new List<string>();
            List<string> state = new List<string>();
            List<string> zipcode = new List<string>();


            name.Add((string)test["name"]);
            rating.Add((double)test["rating"]);
            img.Add((string)test["image_url"]);
            phone.Add((string)test["phone"]);
            address.Add((string)test["location"]["address1"]);
            city.Add((string)test["location"]["city"]);
            state.Add((string)test["location"]["state"]);
            zipcode.Add((string)test["location"]["zip_code"]);




            var FinalList = new

            {

                names = name,
                ratings = rating,
                image = img,
                phones = phone,
                addresss = address,
                citys = city,
                states = state,
                zipcodes = zipcode

            };


            return Json(FinalList, JsonRequestBehavior.AllowGet);
        }




        public JsonResult GetLname()
        {
            string Lname = Request.QueryString["name"];
            var FinalList = new
            {
                Lnames = Lname
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





