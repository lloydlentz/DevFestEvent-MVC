using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.Spreadsheets;

using DevFestEvent.Models;
using System.Configuration;

namespace DevFestEvent.Controllers
{
    public class SpreadsheetController : Controller
    {
        //
        // GET: /Spreadsheet/
        public ActionResult Index()
        {
            SpreadsheetsService service;

            service = new SpreadsheetsService("Spreadsheet-GData-Sample-App");
            service.setUserCredentials(
                ConfigurationManager.AppSettings["GoogleUser"],
                 ConfigurationManager.AppSettings["GoogleUserPassword"]);

            SpreadsheetQuery query = new SpreadsheetQuery();
            var feed = service.Query(query);

            return View(feed);
        }

        public ActionResult AllCells()
        {
            SpreadsheetsService service;

            service = new SpreadsheetsService("DevFestEvent");
            service.setUserCredentials(
                ConfigurationManager.AppSettings["GoogleUser"],
                 ConfigurationManager.AppSettings["GoogleUserPassword"]);
            SpreadsheetQuery q = new SpreadsheetQuery(App.SheetFeedData);
            var feed = service.Query(q);
            AtomLink l = feed.Entries.First().Links.FindService(GDataSpreadsheetsNameTable.WorksheetRel, null);
            WorksheetQuery query = new WorksheetQuery(l.HRef.ToString());
            WorksheetFeed f = service.Query(query);

            foreach (var item in f.Entries)
            {
                AtomLink cellFeedLink = item.Links.FindService(GDataSpreadsheetsNameTable.CellRel, null);
                var cellfeedlink = cellFeedLink.HRef.ToString();

                CellQuery cquery = new CellQuery(cellfeedlink);
                CellFeed cfeed = service.Query(cquery);

                Console.WriteLine("Cells in this worksheet:");
                uint rownum = 2;

                foreach (CellEntry curCell in cfeed.Entries)
                {
                    rownum = curCell.Cell.Row;
                }
            }
            return View(f);


        }

        public ContentResult CSS()
        {
            SpreadsheetsService service;

            service = new SpreadsheetsService("DevFestEvent");
            service.setUserCredentials(
                ConfigurationManager.AppSettings["GoogleUser"],
                 ConfigurationManager.AppSettings["GoogleUserPassword"]);
            var cellfeedlink = App.SheetFeedCSS;
            CellQuery cquery = new CellQuery(cellfeedlink);
            CellFeed cfeed = service.Query(cquery);
            string ans = "";

            foreach (CellEntry curCell in cfeed.Entries)
            {
                ans += curCell.Cell.Value;
            }
            return Content(ans, "text/css");

        }


        public ActionResult Schedule()
        {
            SpreadsheetsService service;

            service = new SpreadsheetsService("DevFestEvent");
            service.setUserCredentials(
                ConfigurationManager.AppSettings["GoogleUser"],
                 ConfigurationManager.AppSettings["GoogleUserPassword"]);
            var cellfeedlink = App.SheetFeedData;

            CellQuery cquery = new CellQuery(cellfeedlink);
            CellFeed cfeed = service.Query(cquery);

            Console.WriteLine("Cells in this worksheet:");
            uint rownum = 2;
            List<Session> sessions = new List<Session>();
            Session workingSession = new Session();

            foreach (CellEntry curCell in cfeed.Entries)
            {
                if (curCell.Cell.Row > 1)
                {
                    if (curCell.Cell.Row != rownum)
                    {
                        sessions.Add(workingSession);
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
                            workingSession.Description = curCell.Cell.Value;
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
                    }
                }
            }
            sessions.Add(workingSession);
            return View(sessions);
        }

        public bool Update(Session model)
        {
            SpreadsheetsService service;

            service = new SpreadsheetsService("DevFestEvent");
            service.setUserCredentials(
                ConfigurationManager.AppSettings["GoogleUser"],
                 ConfigurationManager.AppSettings["GoogleUserPassword"]);
            SpreadsheetQuery q = new SpreadsheetQuery(App.SheetFeedData);
            // Make a request to the API and get all spreadsheets.
            SpreadsheetFeed feed = service.Query(q);
            if (feed.Entries.Count == 0)
            {
                return false;
            }

            // TODO: Choose a spreadsheet more intelligently based on your
            // app's needs.
            SpreadsheetEntry spreadsheet = (SpreadsheetEntry)feed.Entries[0];
            Console.WriteLine(spreadsheet.Title.Text);

            // Get the first worksheet of the first spreadsheet.
            // TODO: Choose a worksheet more intelligently based on your
            // app's needs.
            WorksheetFeed wsFeed = spreadsheet.Worksheets;
            WorksheetEntry worksheet = (WorksheetEntry)wsFeed.Entries[0];

            // Fetch the cell feed of the worksheet.
            CellQuery cellQ = new CellQuery(worksheet.CellFeedLink);
            CellFeed cfeed = service.Query(cellQ);

            Console.WriteLine("Cells in this worksheet:");
            uint rownum = 2;
            List<Session> sessions = new List<Session>();
            Session workingSession = new Session();

            uint updaterow = 0;
            foreach (CellEntry curCell in cfeed.Entries)
            {
                if (curCell.Cell.Row > 1)
                {
                    if (curCell.Cell.Row != rownum)
                    {
                        sessions.Add(workingSession);
                        rownum = curCell.Cell.Row;
                        workingSession = new Session();
                    }
                    switch (curCell.Cell.Column)
                    {
                        case 1:
                            workingSession.IID = int.Parse(curCell.Cell.Value);
                            if (workingSession.IID == model.IID)
                            {
                                updaterow = curCell.Cell.Row;
                            }
                            break;
                        case 2:
                            if (curCell.Cell.Row == updaterow)
                            {
                                curCell.InputValue = model.Title;
                                curCell.Update();
                            }
                            break;
                        case 3:
                            if (curCell.Cell.Row == updaterow)
                            {
                                curCell.InputValue = model.SpeakerDescription;
                                curCell.Update();
                            }
                            break;
                        case 4:
                            if (curCell.Cell.Row == updaterow)
                            {
                                curCell.InputValue = model.Description;
                                curCell.Update();
                            }
                            break;
                        case 5:
                            if (curCell.Cell.Row == updaterow)
                            {
                            curCell.InputValue = model.Room;
                            curCell.Update();
                            }
                            break;
                        case 6:
                            if (curCell.Cell.Row == updaterow)
                            {
                            curCell.InputValue = model.StartHour.ToString();
                            curCell.Update();
                            }
                            break;
                        case 7:
                            if (curCell.Cell.Row == updaterow)
                            {
                            curCell.InputValue = model.StartAMPM;
                            curCell.Update();
                            }
                            break;
                        case 8:
                            if (curCell.Cell.Row == updaterow)
                            {
                            curCell.InputValue = model.LengthMin.ToString();
                            curCell.Update();
                            }
                            break;
                        case 9:
                            if (curCell.Cell.Row == updaterow)
                            {
                                curCell.InputValue = model.CSSClass;
                                curCell.Update();
                            }
                            break;
                    }
                }
            }
            return true;
        }
    }
}