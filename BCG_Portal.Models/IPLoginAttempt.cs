using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCG_Portal.Models
{
    public class IPLoginAttempt
    {
        [Key]
        public int IdIPAdress { get; set; }
        public string IPAdress { get; set; }
        public string UserAgend { get; set; }
        public Nullable<System.DateTime> DateChanged { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }

        public string UserName { get; set; }
        public virtual User User { get; set; }
        public string Country { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
