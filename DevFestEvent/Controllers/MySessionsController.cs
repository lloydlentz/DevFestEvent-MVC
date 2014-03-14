using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using DevFestEvent.Models;
using System.Text;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.Spreadsheets;
using DevFestEvent.ViewModels;


namespace DevFestEvent.Controllers
{
    public class MySessionsController : Controller
    {
        //
        // GET: /MySessions/
        public ActionResult Index()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var s  = db.AppUsers.Find(User.Identity.GetUserId());
            if (s == null)
                return RedirectToAction("Index", "Home");

            db.Entry(s).Collection(c => c.Sessions).Load();
            return View(s);
        }
	}
}