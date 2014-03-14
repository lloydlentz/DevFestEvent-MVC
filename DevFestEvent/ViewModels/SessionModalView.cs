using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevFestEvent.Models;
using Microsoft.AspNet.Identity;

namespace DevFestEvent.ViewModels
{
    public class SessionModalView
    {
        public Session Session { get; set; }
        public AppUser AppUser { get; set; }
        public bool alreadyAdded { get; set; }
    }
}