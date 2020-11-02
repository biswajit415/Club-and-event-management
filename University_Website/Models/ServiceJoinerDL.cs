using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace University_Website.Models
{
    public class ServiceJoinerDL
    {
        [Key]
        public int ServiceJoinerID { get; set; }

        public string Slot { get; set; }
        public int UserID { get; set; }
        public UserDL Users { get; set; }

        public int ServiceID { get; set; }
        public ServiceDL Services { get; set; }

    }
}