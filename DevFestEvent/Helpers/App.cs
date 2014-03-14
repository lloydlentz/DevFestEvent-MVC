using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DevFestEvent.Models
{
    public class App
    {
        
        public static string SheetUrl 
        { 
            get { 
                return String.Format( "https://docs.google.com/spreadsheet/ccc?key={0}&usp=drive_web#gid=0"
                    , ConfigurationManager.AppSettings["GoogleSheetKey"]);
            } 
        }
        public static string SheetFeedData
        {
            get
            {
                return String.Format("https://spreadsheets.google.com/feeds/cells/{0}/od6/private/full"
                    , ConfigurationManager.AppSettings["GoogleSheetKey"]);
            }
        }
        public static string SheetFeedCSS
        {
            get
            {
                return String.Format("https://spreadsheets.google.com/feeds/cells/{0}/od7/private/full"
                    , ConfigurationManager.AppSettings["GoogleSheetKey"]);
            }
        }
    }
}