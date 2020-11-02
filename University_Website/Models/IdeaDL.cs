using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;


namespace University_Website.Models
{
    [Table("IdeaTbl")]
    public class IdeaDL
    {
        [Key]
        public int IdeaID { get; set; }

        [Required(ErrorMessage = " Ttile is Required ")]
        [DisplayName("Idea Title")]
        public string IdeaTitle { get; set; }

        [Required(ErrorMessage = " Description is Required ")]
        [DisplayName("Description ")]
        public string IdeaDescription { get; set; }

        public DateTime PostIdeaDate { get; set; }


        [DisplayName("UpVote")]
        public int UpVoteCount { get; set; }

        [DisplayName("DownVote")]
        public int DownVoteCount { get; set; }

        [Required(ErrorMessage = " Club name is Required ")]
        [DisplayName("Club Name")]
        public string ClubName { get; set; }

        [Required(ErrorMessage = " Category is Required ")]
        public string Category { get; set; }


        public string Approve { get; set; }
        public int UserID { get; set; }
        public UserDL Users { get; set; }

        public int ClubID { get; set; }
        public ClubDL Club {get;set;}
        public List<UserActivityOnIdea> UserActivityOnIdeas { get; set; }

        public List<ShareIdeaContentDL> ShareIdeaContents { get; set; }
    }
}