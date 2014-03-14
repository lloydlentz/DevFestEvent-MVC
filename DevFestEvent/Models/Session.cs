using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DevFestEvent.Models
{
    public class Session
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int IID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Sort { get; set; }
        public string Room { get; set; }
        //public DateTime Start { get; set; }
        public int StartHour { get; set; }
        public string StartMinute { get; set; }
        public string StartAMPM { get; set; }
        public int LengthMin { get; set; }
        public string SpeakerDescription { get; set; }
        public string CSSClass { get; set; }
        public string CustomURL { get; set; }

        public string FormattedStartTime
        {
            get
            {
                string ans = String.Concat(StartHour, StartAMPM);
                if (String.IsNullOrEmpty(StartMinute))
                    StartMinute = "00";
                if (StartMinute != "00")
                {
                    ans = String.Concat(StartHour, ":", StartMinute, StartAMPM);
                }
                return ans;
            }
        }

        public virtual ICollection<Speaker> Speakers { get; set; }
        public virtual ICollection<AppUser> AppUsers { get; set; }

    }
}