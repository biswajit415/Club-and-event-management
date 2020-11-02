using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace University_Website.Models
{
    public class JoinSubmitForm
    {

        [Required(ErrorMessage = "User Id is required")]
        [DisplayName("User Id")]
        public int UserID { get; set; }


        [Required(ErrorMessage = "Designation is required")]

        public string Designation { get; set; }
      


    }
}