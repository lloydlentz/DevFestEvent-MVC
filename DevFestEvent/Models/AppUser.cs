using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevFestEvent.Models
{
    public class AppUser
    {
        [Key]
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Admin { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }
    }
}