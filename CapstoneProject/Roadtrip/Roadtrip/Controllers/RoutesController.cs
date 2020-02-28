using System;

using System.Collections.Generic;


using System.Linq;

using System.Web;





using System.Web.Mvc;

using System.Diagnostics;

using System.IO;

using System.Net;

using Newtonsoft.Json.Linq;


using System.Web.Helpers;




using System.Web.Helpers;


using Microsoft.Ajax.Utilities;

using Newtonsoft.Json;


using Roadtrip.Models.ViewModels;



using Roadtrip.Models.ViewModels;


namespace Roadtrip.Controllers

{

    public class RoutesController : Controller

    {

        // GET: Routes


        [HttpGet]


        [HttpGet]

        public ActionResult Index()

        {

            return View();

        }
<<<<<<< HEAD


=======
>>>>>>> dev

        public ActionResult Create()

        {

            return View();

        }



        public JsonResult GetEstablishment()

        {
<<<<<<< HEAD

            string city = Request.QueryString["city"];

            string state = Request.QueryString["state"];

            string term = Request.QueryString["name"];

            string radius = Request.QueryString["numbers"];

            //string key = System.Web.Configuration.WebConfigurationManager.AppSettings["YelpKey"];

            string key = "3glYwaLZjmtLvAcgvmia-ocJ1tdhu6PAFCo0jCYrmsgHXZXX0tduCis8dKk3GMGO7Oc9jYYRLTPRSaWopVeUJMI8pjCj2nNcjDhh1mcYsMA3xjkndOqPba6k3_dOXnYx";

            string uri = "https://api.yelp.com/v3/businesses/search?location=" + city + "," + state + "&radius=" + radius + "&term=" + term;

=======
            string city = Request.QueryString["city"];
            string state = Request.QueryString["state"];
            string term = Request.QueryString["name"];
            string radius = Request.QueryString["numbers"];
            //string key = System.Web.Configuration.WebConfigurationManager.AppSettings["YelpKey"];
            string key = "3glYwaLZjmtLvAcgvmia-ocJ1tdhu6PAFCo0jCYrmsgHXZXX0tduCis8dKk3GMGO7Oc9jYYRLTPRSaWopVeUJMI8pjCj2nNcjDhh1mcYsMA3xjkndOqPba6k3_dOXnYx";
            string uri = "https://api.yelp.com/v3/businesses/search?location=" + city + "," + state + "&radius=" + radius + "&term=" + term;
>>>>>>> dev
            string data = SendRequest(uri, key);



            JObject test = JObject.Parse(data);

            List<string> names = new List<string>();

            List<double> index = new List<double>();

            List<double> ratings = new List<double>();

            List<decimal> longi = new List<decimal>();

            List<decimal> lati = new List<decimal>();

            List<string> BusinessID = new List<string>();
            int count = (int)test["total"];

<<<<<<< HEAD
            int count = (int)test["total"];



            if (count > 20)

            {

                count = 20;

            }



            for (int i = 0; i < count; i++)

=======
            if (count > 20)
>>>>>>> dev
            {
                count = 20;
            }

            for (int i = 0; i < count; i++)
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
<<<<<<< HEAD

                id = BusinessID,

                total = count

=======
                id = BusinessID,
                total = count
>>>>>>> dev
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
<<<<<<< HEAD


=======
>>>>>>> dev

            List<string> name = new List<string>();

            List<double> rating = new List<double>();

            List<string> img = new List<string>();

            List<string> phone = new List<string>();

            List<string> address = new List<string>();

            List<string> city = new List<string>();

            List<string> state = new List<string>();

            List<string> zipcode = new List<string>();
<<<<<<< HEAD


=======
>>>>>>> dev

            name.Add((string)test["name"]);

            rating.Add((double)test["rating"]);
<<<<<<< HEAD

=======
>>>>>>> dev
            img.Add((string)test["image_url"]);

            phone.Add((string)test["phone"]);

            address.Add((string)test["location"]["address1"]);

            city.Add((string)test["location"]["city"]);

            state.Add((string)test["location"]["state"]);

<<<<<<< HEAD
            zipcode.Add((string)test["location"]["zip_code"]);



=======
>>>>>>> dev
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
<<<<<<< HEAD

=======
>>>>>>> dev
            return Json(FinalList, JsonRequestBehavior.AllowGet);

        }

        private string SendRequest(string uri, string key)

<<<<<<< HEAD

        private string SendRequest(string uri, string key)



        {



            Debug.WriteLine(uri);



            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);



            request.Headers.Add("Authorization", "Bearer " + key);



=======
        {

            Debug.WriteLine(uri);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);

            request.Headers.Add("Authorization", "Bearer " + key);

>>>>>>> dev
            request.Accept = "application/json";



<<<<<<< HEAD




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



        public ActionResult DisplayInfo(string myInfo, string city)

=======
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

        public ActionResult DisplayInfo(string myInfo, string city)
>>>>>>> dev
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

<<<<<<< HEAD


            string urlPlace = "https://api.opencagedata.com/geocode/v1/json?q=" + test + "+" + myCity + "+" + myState + "&key=" + myKey;

            //string urlCity = "https://api.opencagedata.com/geocode/v1/json?q=" + myCity + "&key=3e00b526f7af428a93598818cf2e926d";

            string json = SendRequestToken(urlPlace, myKey);

=======
            string urlPlace = "https://api.opencagedata.com/geocode/v1/json?q=" + test + "+" + myCity + "+" + myState + "&key=" + myKey;
            //string urlCity = "https://api.opencagedata.com/geocode/v1/json?q=" + myCity + "&key=3e00b526f7af428a93598818cf2e926d";
            string json = SendRequestToken(urlPlace, myKey);
>>>>>>> dev
            //string jsonCity = SendRequest(urlCity, key); 

            JObject mapInfo = JObject.Parse(json);
<<<<<<< HEAD

            // JObject cityInfo = JObject.Parse(jsonCity);


=======
            // JObject cityInfo = JObject.Parse(jsonCity);
>>>>>>> dev

            //[JSON].results.[0].bounds.northeast.lat

            //[JSON].results.[0].bounds.northeast.lng







            //string lat =  [JSON].results.[0].bounds.northeast.lat;

            //string lon =  [JSON].results.[0].bounds.northeast.lng; 

            string lat = (string)mapInfo.SelectToken("results.[0].bounds.northeast.lat");

            string lon = (string)mapInfo.SelectToken("results.[0].bounds.northeast.lng");

<<<<<<< HEAD


            /* string cityLat = (string)cityInfo.SelectToken("results.[0].bounds.northeast.lat");

             string cityLon = (string)cityInfo.SelectToken("results.[0].bounds.northeast.lng");*/


=======
            /* string cityLat = (string)cityInfo.SelectToken("results.[0].bounds.northeast.lat");
             string cityLon = (string)cityInfo.SelectToken("results.[0].bounds.northeast.lng");*/
>>>>>>> dev

            MapInfoViewModel updateInfo = new MapInfoViewModel()

            {
<<<<<<< HEAD

                Lat = lat,

                Lon = lon,



            };





=======
                Lat = lat,
                Lon = lon,

            };

>>>>>>> dev


            return new ContentResult

            {



                Content = JsonConvert.SerializeObject(updateInfo),

                ContentType = "application/json",

                ContentEncoding = System.Text.Encoding.UTF8

            };

        }

<<<<<<< HEAD


=======
>>>>>>> dev
        private string SendRequestToken(string uri, string credentials)

        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);

            request.Headers.Add("Authorization", "token " + credentials);

            request.Accept = "application/json";



            string jsonString = null;

<<<<<<< HEAD


=======
>>>>>>> dev
            // TODO: You should handle exceptions here

            using (WebResponse response = request.GetResponse())

            {

                Stream stream = response.GetResponseStream();

                StreamReader reader = new StreamReader(stream);

                jsonString = reader.ReadToEnd();

                reader.Close();
<<<<<<< HEAD
=======
                stream.Close();
            }
            return jsonString;
        }

    }
}
>>>>>>> dev

                stream.Close();

            }

            return jsonString;

        }



    }

<<<<<<< HEAD
}
=======


>>>>>>> dev
