using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace University_Website.Models
{
    [Table("ClubTbl")]
    public class ClubDL
    {
        [Key]
        public int ClubID { get; set; }

        [Required(ErrorMessage = "Club Name Is Required")]
        [DisplayName("ClubName")]
        public string ClubName { get; set; }

        [Required(ErrorMessage = "Eligibility Criteria Is Required")]
        [DisplayName("Eligibility")]
        public string Eligibility { get; set; }

        [Required(ErrorMessage = "Description Is Required")]
        [DisplayName("Club Description")]
        public string ClubDescription { get; set; }

        [Required(ErrorMessage = "Designations Is Required")]
        public string Designations { get; set; }

        public List<ClubMemberDL> clubMembers { get; set; }

    }
}