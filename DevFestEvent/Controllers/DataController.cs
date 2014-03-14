using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevFestEvent.Models;
using DevFestEvent.ViewModels;

namespace DevFestEvent.Controllers
{
    public class DataController : Controller
    {
        //
        // GET: /Data/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SessionNumbers()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var s = db.Sessions.Where(r=> r.AppUsers.Count > 0)
                .GroupBy(r => r.IID)
                .Select(gr=> new {Key = (int)gr.Key, Count = gr.Count()})
                .ToList();

            List<DataSessionNumbersView> l = new List<DataSessionNumbersView>();
            foreach (var i in s)
            {
                var ses = db.Sessions.Where(q=>q.IID == i.Key).FirstOrDefault();
                if (ses != null){
                    l.Add(new DataSessionNumbersView(){
                     Title = ses.Title,
                     Time = ses.FormattedStartTime,
                     Room = ses.Room,
                     Count = i.Count
                    });
                    
                }
            }

            return View(l);
        }
    }
}