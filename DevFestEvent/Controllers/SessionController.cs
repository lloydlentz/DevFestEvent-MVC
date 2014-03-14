using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DevFestEvent.Models;
using DevFestEvent.Helpers;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Validation;
using DevFestEvent.ViewModels;

namespace DevFestEvent.Controllers
{
    public class SessionController : Controller
    {
        //
        // GET: /Session/
        public String Index()
        {
            return "hi";
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationDbContext db = new ApplicationDbContext();
            var s = db.Sessions.Where(q => q.IID == id).FirstOrDefault();
            var u = db.AppUsers.Find(User.Identity.GetUserId());
            if (s == null)
                return HttpNotFound();
            if (!u.Admin)
                return RedirectToAction("Index", "Home");

            return View(s);
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Session model)
        {
            if (ModelState.IsValid)
            {

                ApplicationDbContext db = new ApplicationDbContext();
                var s = db.Sessions.Where(q => q.IID == model.IID).FirstOrDefault();
                var u = db.AppUsers.Find(User.Identity.GetUserId());
                if (s == null)
                    return HttpNotFound();
                if (!u.Admin)
                    return RedirectToAction("Index", "Home");

                s.Description = model.Description;
                db.Entry(s).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Details", "Session", new { id = model.IID });
            }

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditFull(Session model) //Cannot seem to get the spreadsheet to take the update.
        {
            if (ModelState.IsValid)
            {

                if (new SpreadsheetController().Update(model))
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }




        public ActionResult Details(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index","Home");
            }

            ApplicationDbContext db = new ApplicationDbContext();

            var u = db.AppUsers.Find(User.Identity.GetUserId());
            var s = db.Sessions.Where(q => q.CustomURL == id).FirstOrDefault();
            if (s == null)
            {
                int i = 0;
                if (int.TryParse(id,out i)){
                    s = db.Sessions.Where(q => q.IID == i).FirstOrDefault();
                    if (s == null)
                        return HttpNotFound();
                }
            }

            var sm = new SessionModalView()
            {
                Session = s,
                AppUser = u
            };
            if (u != null)
            {
                db.Entry(u).Collection(c => c.Sessions).Load();
                List<int> ls = new List<int>();
                foreach (var ses in u.Sessions) { ls.Add(ses.IID); }
                sm.alreadyAdded = ls.Contains(s.IID);
            }

            return View(sm);
        }
        public ActionResult DetailsModal(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationDbContext db = new ApplicationDbContext();
            var s = db.Sessions.Where(q => q.IID == id).FirstOrDefault();
            var u = db.AppUsers.Find(User.Identity.GetUserId());
            if (s == null)
            {
                return HttpNotFound();
            }

            var sm = new SessionModalView()
            {
                Session = s,
                AppUser = u
            };
            if (u != null)
            {
                db.Entry(u).Collection(c => c.Sessions).Load();
                List<int> ls = new List<int>();
                foreach (var ses in u.Sessions) { ls.Add(ses.IID); }
                sm.alreadyAdded = ls.Contains(s.IID);
            }

            return View(sm);
        }

        public String Add(int? id)
        {
            if (id == null)
                return "";

            ApplicationDbContext db = new ApplicationDbContext();
            var u = db.AppUsers.Find(User.Identity.GetUserId());
            var s = DevFestEvent.Helpers.SessionHelper.AllSessions().Where(q => q.IID == id).FirstOrDefault();
            if (s == null)
                return "";

            u.Sessions.Add(s);
            db.Entry(u).State = System.Data.Entity.EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return "ok";
        }
        public String Del(int? id)
        {
            if (id == null)
                return "";

            ApplicationDbContext db = new ApplicationDbContext();
            var u = db.AppUsers.Find(User.Identity.GetUserId());
            db.Entry(u).Collection(c => c.Sessions).Load();
            var s = u.Sessions.Where(q => q.IID == id);
            if (s == null)
                return "";
            if (u == null)
                return "";

            foreach (var ses in s.ToList())
            {
                u.Sessions.Remove(ses);
            }
            db.Entry(u).State = System.Data.Entity.EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return "ok";
        }




	}
}