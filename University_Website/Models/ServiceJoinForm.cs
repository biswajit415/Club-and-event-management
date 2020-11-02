using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace University_Website.Models
{
    public class ServiceJoinForm
    {
        [Required(ErrorMessage ="User Id is required")]
        [DisplayName("User Id")]
        public int UserID { get; set; }


        [Required(ErrorMessage = "Phone Number is required")]
        [DisplayName("Phone Number")]
        public long PhoneNumber { get; set; }


        [Required(ErrorMessage = "Please Choose your slot")]
        [DisplayName("Slots")]
        public string Slot { get; set; }

    }
}