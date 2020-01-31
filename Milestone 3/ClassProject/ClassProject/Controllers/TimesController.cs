using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClassProject.Models;
using ClassProject.Models.ViewModels;

namespace ClassProject.Controllers
{
    public class TimesController : Controller
    {
        private ClassContext db = new ClassContext();

        // GET: Times
        public ActionResult Index()
        {
            var times = db.Times.Include(t => t.Athlete).Include(t => t.Event);
            return View(times.ToList().OrderByDescending(r => r.Athlete.Name).Distinct());
        }

        // GET: Times/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Time time = db.Times.Find(id);
            if (time == null)
            {
                return HttpNotFound();
            }
            /*var my = db.Times.Join(db.Athletes.Where(
                r => r.AID == time.AthleteID),
                e => e.AthleteID, r => r.AID,
                (e, r) => new { e, r }).Select( m => new athTimeViewModel
                {
                   
                    Times = m.r.Times

                }) ;*/
            var mytimes = db.Times.Where(a => a.AthleteID == time.AthleteID).Include(t => t.Athlete).Include(t => t.Event); 
            
            ViewBag.tam = time.Athlete.Name;
           

            return View(mytimes.ToList());
        }

        // GET: Times/Create
        public ActionResult Create()
        {
            ViewBag.AthleteID = new SelectList(db.Athletes, "AID", "Name");
            ViewBag.EventID = new SelectList(db.Events, "EID", "EID");
            return View();
        }

        // POST: Times/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TID,AthleteID,EventID,Time1")] Time time)
        {
            if (ModelState.IsValid)
            {
                db.Times.Add(time);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AthleteID = new SelectList(db.Athletes, "AID", "Name", time.AthleteID);
            ViewBag.EventID = new SelectList(db.Events, "EID", "EID", time.EventID);
            return View(time);
        }

        // GET: Times/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Time time = db.Times.Find(id);
            if (time == null)
            {
                return HttpNotFound();
            }
            ViewBag.AthleteID = new SelectList(db.Athletes, "AID", "Name", time.AthleteID);
            ViewBag.EventID = new SelectList(db.Events, "EID", "EID", time.EventID);
            return View(time);
        }

        // POST: Times/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TID,AthleteID,EventID,Time1")] Time time)
        {
            if (ModelState.IsValid)
            {
                db.Entry(time).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AthleteID = new SelectList(db.Athletes, "AID", "Name", time.AthleteID);
            ViewBag.EventID = new SelectList(db.Events, "EID", "EID", time.EventID);
            return View(time);
        }

        // GET: Times/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Time time = db.Times.Find(id);
            if (time == null)
            {
                return HttpNotFound();
            }
            return View(time);
        }

        // POST: Times/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Time time = db.Times.Find(id);
            db.Times.Remove(time);
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
