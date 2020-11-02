using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace University_Website.Models
{
    public class ShareFormDL
    {

        [Required(ErrorMessage = "User Id  is Required ")]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Phone Number is Required ")]
        public long PhoneNumber { get; set; }
    }
}