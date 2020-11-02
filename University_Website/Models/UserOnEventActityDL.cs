using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace University_Website.Models
{
    [Table("UserOnEventActityTbl")]
    public class UserOnEventActityDL
    {
        [Key]
        public int UserActivityId { get; set; }

        
        public string Attendence { get; set; }

        public int Like { get; set; }

        public int Dislike { get; set; }

        public string Interested { get; set; }

        public string NotInterested{ get; set; }


        public int EventID { get; set; }
        public EventDL Events { get; set; }

        public int UserID { get; set; }
        public UserDL Users { get; set; }

    }
}