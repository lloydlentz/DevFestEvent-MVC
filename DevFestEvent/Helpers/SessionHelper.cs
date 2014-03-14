using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.Spreadsheets;

using DevFestEvent.Models;
using System.Data.Entity.Validation;
using System.Configuration;

namespace DevFestEvent.Helpers
{
    public class SessionHelper
    {

        public static List<Session> AllSessions()
        {
            SpreadsheetsService service;

            service = new SpreadsheetsService("DevFestEvent");            
            service.setUserCredentials(
                ConfigurationManager.AppSettings["GoogleUser"], 
                 ConfigurationManager.AppSettings["GoogleUserPassword"]);
            CellQuery cquery = new CellQuery(App.SheetFeedData);
            CellFeed cfeed = service.Query(cquery);

            //Row1 has the header rows, so we sant to start with row2
            uint rownum = 2;
            
            List<Session> sessions = new List<Session>();
            Session workingSession = new Session();
            foreach (CellEntry curCell in cfeed.Entries)
            {
                if (curCell.Cell.Row > 1)
                {
                    if (curCell.Cell.Row != rownum)
                    {
                        if (!String.IsNullOrEmpty(workingSession.Title)) { 

                            sessions.Add(workingSession);
                            UpdateOrCreateSession(workingSession);
                        }
                        rownum = curCell.Cell.Row;
                        workingSession = new Session();
                    }
                    switch (curCell.Cell.Column)
                    {
                        case 1:
                            workingSession.IID = int.Parse(curCell.Cell.Value);
                            break;
                        case 2:
                            workingSession.Title = curCell.Cell.Value;
                            break;
                        case 3:
                            workingSession.SpeakerDescription = curCell.Cell.Value;
                            break;
                        case 4:
                            workingSession.Sort = curCell.Cell.Value;
                            break;
                        case 5:
                            workingSession.Room = curCell.Cell.Value;
                            break;
                        case 6:
                            workingSession.StartHour = int.Parse(curCell.Cell.Value);
                            break;
                        case 7:
                            workingSession.StartAMPM = curCell.Cell.Value;
                            break;
                        case 8:
                            workingSession.LengthMin = int.Parse(curCell.Cell.Value);
                            break;
                        case 9:
                            workingSession.CSSClass = curCell.Cell.Value;
                            break;
                        case 10:
                            workingSession.CustomURL = curCell.Cell.Value;
                            break;
                        case 11:
                            int min = 0;
                            int.TryParse(curCell.Cell.Value??"0", out min);
                            workingSession.StartMinute = min.ToString().PadLeft(2,'0');
                            break;
                    }
                }
            }
            sessions.Add(workingSession);
            UpdateOrCreateSession(workingSession);
          
            return sessions;
        }

        static void UpdateOrCreateSession(Session workingSession)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var s = db.Sessions.Where(q => q.IID == workingSession.IID).FirstOrDefault();
            if (s != null)
            {
                //s.Description = workingSession.Description;
                s.CSSClass = workingSession.CSSClass;
                s.LengthMin = workingSession.LengthMin;
                s.Sort = workingSession.Sort;
                s.Room = workingSession.Room;
                s.SpeakerDescription = workingSession.SpeakerDescription;
                s.StartAMPM = workingSession.StartAMPM;
                s.StartHour = workingSession.StartHour;
                s.StartMinute = workingSession.StartMinute;
                s.Title = workingSession.Title;
                s.CustomURL = workingSession.CustomURL;
                db.Entry(s).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                db.Sessions.Add(workingSession);
            }
            try
            {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges

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

        }
    }
}