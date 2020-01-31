using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClassProject.Models;

namespace ClassProject.Controllers
{
    public class AthletesController : Controller
    {
        private ClassContext db = new ClassContext();

        // GET: Athletes
        public ActionResult Index()
        {
            return View(db.Athletes.ToList());
        }

        public ActionResult Search()
        {
            return View(db.Athletes.ToList());
        }

        [HttpPost]
        public ActionResult Search(string Search)
        {
            //var athletes = db.Athletes.Include(s => s.Name).Include(s => s.DOB).Include(s => s.AthleteTeams); 
            var athletes = db.Athletes.Where(s => s.Name.Contains(Search));

            return View(athletes.ToList());
        }


        // GET: Athletes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Athlete athlete = db.Athletes.Find(id);
            if (athlete == null)
            {
                return HttpNotFound();
            }

            var times = db.Times.Where(s => s.AthleteID.Equals(athlete.AID));

            ViewBag.times = times.ToList();
            return View(athlete);
        }

        // GET: Athletes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Athletes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AID,Name,DOB")] Athlete athlete)
        {
            if (ModelState.IsValid)
            {
                db.Athletes.Add(athlete);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(athlete);
        }

        // GET: Athletes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Athlete athlete = db.Athletes.Find(id);
            if (athlete == null)
            {
                return HttpNotFound();
            }
            return View(athlete);
        }

        // POST: Athletes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AID,Name,DOB")] Athlete athlete)
        {
            if (ModelState.IsValid)
            {
                db.Entry(athlete).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(athlete);
        }

        // GET: Athletes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Athlete athlete = db.Athletes.Find(id);
            if (athlete == null)
            {
                return HttpNotFound();
            }
            return View(athlete);
        }

        // POST: Athletes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Athlete athlete = db.Athletes.Find(id);
            db.Athletes.Remove(athlete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PrivacyPolicy()
        {
            return View();
        }
        public ActionResult FAQ()
        {
            return View();
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
