using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevFestEvent.Models;
using Microsoft.AspNet.Identity;

namespace DevFestEvent.ViewModels
{
    public class DataSessionNumbersView
    {
        public Session Session { get; set; }
        public string Title { get; set; }
        public string Time { get; set; }
        public string Room { get; set; }
        public int Hour { get; set; }
        public string AMPM { get; set; }
        public string Min { get; set; }
        public int Count { get; set; }
    }
}