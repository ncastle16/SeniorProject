using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Roadtrip.Models;

namespace Roadtrip.Controllers
{
    public class ProfilesController : Controller
    {
        private ProfileContext db = new ProfileContext();

        // GET: Profiles
        public ActionResult Index()
        {
            return View(db.Profiles.ToList());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(Profile profile)
        {
            string name = User.Identity.Name;
            Profile temp = db.Profiles.FirstOrDefault(s => s.UserName.Equals(name));
            string st = profile.AboutMe;
            temp.AboutMe = st;
            
            db.SaveChanges();

            return View(temp);
        }

        public ActionResult Follow(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.First(s => s.UserName.Contains(User.Identity.Name));
            if (profile == null)
            {
                return HttpNotFound();
            }

            int result = ParseFList(profile.Following, "[F]").IndexOf(id);
            if (result == -1)
            {
                
                StringBuilder sb = new StringBuilder();
                sb.Append(profile.Following);
                sb.AppendFormat("[F]"+ id + "[F] \n");
                profile.Following = sb.ToString();
                db.SaveChanges();
            }

            Profile profileToFollow = db.Profiles.First(s => s.UserName.Contains(id));
            result = ParseFList(profileToFollow.Follower, "[F]").IndexOf(User.Identity.Name);
            if (result == -1)
            {

                StringBuilder sb = new StringBuilder();
                sb.Append(profileToFollow.Follower);
                sb.AppendFormat("[F]" + User.Identity.Name + "[F]\n");
                profileToFollow.Follower = sb.ToString();
                db.SaveChanges();
            }

            return Details(profile);
        }


        public List<string> ParseFList(string s, string delimiter)
        {
            List<string> strings = new List<string>();
            int start, end;
            //Split the stored string into an array
            string[] words = s.Split('\n');

            //Foreach location-
            for (int i = 0; i < words.Length - 1; i++)
            {
                if (words[i].Contains(delimiter))
                {
                    start = words[i].IndexOf(delimiter);
                    end = words[i].LastIndexOf(delimiter);
                    strings.Add(words[i].Substring(start + 3, end - start - 3));
                }
            }

            return strings;
        }



        [HttpGet]
        // GET: Profiles/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return Content("oops, user profile not specified");
            }
            Profile profile = db.Profiles.FirstOrDefault(s => s.UserName.Equals(id));

            if (profile.Following != null)
                profile.FollowingList = ParseFList(profile.Following, "[F]");

            if (profile.Follower != null)
                profile.FollowerList = ParseFList(profile.Follower, "[F]");


            if (profile == null)
            {
                return Content("oops, looks like this users profile page isn't set up yet.");
            }
            return View(profile);
        }

        // GET: Profiles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PPID,UserName,Friends,AboutMe,PrivacyFlag")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                db.Profiles.Add(profile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(profile);
        }

        // GET: Profiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PPID,UserName,Friends,AboutMe,PrivacyFlag")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profile);
        }

        // GET: Profiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Profile profile = db.Profiles.Find(id);
            db.Profiles.Remove(profile);
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
