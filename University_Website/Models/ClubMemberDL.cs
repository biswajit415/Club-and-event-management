using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace University_Website.Models
{
    public class ClubMemberDL
    {
        [Key]
        public int ClubMemberID { get; set; }

        [Required(ErrorMessage = "Designation is required")]

        public string Designation { get; set; }
        public int ClubID { get; set; }
        public ClubDL Clubs { get; set; }

        [Required(ErrorMessage ="User Id is required")]
        [DisplayName("User Id")]
        public int UserID { get; set; }

        public UserDL Users { get; set; }
    }
}