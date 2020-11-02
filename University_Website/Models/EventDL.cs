using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;



namespace University_Website.Models
{
    [Table("EventTbl")]
    public class EventDL
    {
        [Key]
        [DisplayName("Event ID")]
        public int EventID { get; set; }

        [Required(ErrorMessage = " Event Name is Required ")]
        [DisplayName("Event Name")]
        public string EventName { get; set; }

        [Required(ErrorMessage = " Description is Required ")]
        [DisplayName("Event Description")]
        public string EventDescription { get; set; }

        [Required(ErrorMessage = " Start Date is Required ")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Srart Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = " End Date is Required ")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = " Club is Required ")]
        public string ClubName { get; set; }
        [Required(ErrorMessage = " Club is Required ")]

        public int LikeCount { get; set; }

        public int DisLikeCount { get; set; }

        [Required(ErrorMessage = " Category is Required ")]
        public string Category { get; set; }
      
        
        public List<UserOnEventActityDL> UserOnEventActitys { get; set; }

        public List<PostCommentDL> PostComments { get; set; }
        public List<SharedContent> SharedContents { get; set; }
    }
}