using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace University_Website.Models
{
    [Table("ServiceTbl")]
    public class ServiceDL
    {
        [Key]
        public int ServiceID { get; set; }

        [Required(ErrorMessage ="Service Name is required")]
        [DisplayName("Sevice Name")]
        public string ServiceName { get; set; }


        [Required(ErrorMessage ="Sevice description is required")]
        [DisplayName("Description")]
        public string ServiceDescription { get; set; }

        [Required(ErrorMessage = "number of volunteer is required")]
        [DisplayName(" Volunteer ")]
        public int Volunteer { get; set; }


        [Required(ErrorMessage ="Slot is reqiuired")]
        [DisplayName(" Slot ")]
        public string Slots { get; set; } 


    

        public List<ServiceJoinerDL> ServiceJoiners { get; set; }

    }
}