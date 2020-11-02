using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace University_Website.Models
{
    public class ShareEventStoryDL
    {

        [Key]
        public int ShareEventStoryID { get; set; }


        [Required(ErrorMessage = " Event Name is Required ")]
        [DisplayName("Event Name")]
        public string EventName { get; set; }

        [Required(ErrorMessage = " Description is Required ")]
        [DisplayName("Event Description")]
        public string EventDescription { get; set; }
    }
}