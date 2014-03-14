using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DevFestEvent.Models
{
    public class Speaker
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(150, MinimumLength = 1)]
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<string> Links { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }
    }
}