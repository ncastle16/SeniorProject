using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Roadtrip.DAL;

namespace Roadtrip.Controllers
{



public struct RLocation
    {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Id { get; set; }
    };
    public class SavedRoutesController : Controller
    {
        private SavedRoutesModel db = new SavedRoutesModel();

        [HttpPost]
        public JsonResult SaveRoute(List<RLocation> rl)
        {
            StringBuilder sb = new StringBuilder();
            SavedRoute savedRoute = new SavedRoute();

            foreach (RLocation r in rl) 
            { 
                sb.AppendFormat("[Na]{0}[Na] [La]{1}[La] [Lo]{2}[Lo] [Id]{3}[Id] \n",
                    r.Name, r.Latitude, r.Longitude, r.Id);
            }

            if (!db.SavedRoutes.Any())
            {
               // savedRoute.SRID = 0;
            }

                //savedRoute.SRID = db.SavedRoutes.OrderByDescending(s => s.SRID).FirstOrDefault().SRID;

            if (Request.IsAuthenticated)
                savedRoute.Username = User.Identity.Name;
            else
                savedRoute.Username = "test123@wou.com";
            savedRoute.Timestamp = DateTime.UtcNow;
            savedRoute.Route = sb.ToString();

            db.SavedRoutes.Add(savedRoute);
            
            try
            {
                db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }

            return Json(new { result = "Redirect", url = Url.Action("/Details/" + savedRoute.SRID ) });
        }

        public void LoadRoute(string s)
        {
            RLocation rl;
            int start, end;

            //Split the stored string into an array of locations
            string[] words = s.Split('\n');

            //Foreach location-
            for (int i = 0; i < words.Length; i++)
            {
                //Reset our placeholder
                rl = new RLocation();

                //Set the start position to the first instance of [lat]
                start = words[i].IndexOf("[Na]");
                //Set the end position to the last instance of [lat]
                end = words[i].LastIndexOf("[Na]");

                //Pull out the variable from between the two positions taking into account the tag sizes
                rl.Latitude = double.Parse(words[i].Substring(start + 5, end - start - 5));

                //Repeat for others

            }
        }


        // GET: SavedRoutes
        public ActionResult Index()
        {


            return View(db.SavedRoutes.ToList());
        }

        // GET: SavedRoutes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavedRoute savedRoute = db.SavedRoutes.Find(id);
            if (savedRoute == null)
            {
                return HttpNotFound();
            }
            return View(savedRoute);
        }

        // GET: SavedRoutes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SavedRoutes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SRID,Route,Timestamp,Username")] SavedRoute savedRoute)
        {
            if (ModelState.IsValid)
            {
                db.SavedRoutes.Add(savedRoute);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(savedRoute);
        }

        // GET: SavedRoutes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavedRoute savedRoute = db.SavedRoutes.Find(id);
            if (savedRoute == null)
            {
                return HttpNotFound();
            }
            return View(savedRoute);
        }

        // POST: SavedRoutes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SRID,Route,Timestamp,Username")] SavedRoute savedRoute)
        {
            if (ModelState.IsValid)
            {
                db.Entry(savedRoute).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(savedRoute);
        }

        // GET: SavedRoutes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavedRoute savedRoute = db.SavedRoutes.Find(id);
            if (savedRoute == null)
            {
                return HttpNotFound();
            }
            return View(savedRoute);
        }

        // POST: SavedRoutes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SavedRoute savedRoute = db.SavedRoutes.Find(id);
            db.SavedRoutes.Remove(savedRoute);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
