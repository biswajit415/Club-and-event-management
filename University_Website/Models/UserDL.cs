using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;


namespace University_Website.Models
{
    [Table("UserTbl")]
    public class UserDL
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = " User ID Required ")]
        [DisplayName("User ID")]
        public int UserID { get; set; }

        [Required(ErrorMessage = " First Name Required ")]
        [DisplayName("First Name")]
         public string FirstName { get; set; }


        [Required(ErrorMessage = " Last Name Required ")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }


        [Required(ErrorMessage = " Date of Birth Required ")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Date of Birth")]
        public DateTime DoB { get; set; }


        [Required(ErrorMessage = " Please choose the gender ")]
        [DisplayName("Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = " Phone Number is Required ")]
        [RegularExpression(@"[9876]{1}[0-9]{9}", ErrorMessage = "* Mobile Number Format Wrong")]
        [DisplayName("Phone Number")]
        public long PhoneNumber { get; set; }


        [Required(ErrorMessage = " Email is Required ")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        
        [Required(ErrorMessage = " Password is Required ")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "* Password should have special character  , lower case and upper case and number\r\n" +
            "and Password length must be 8 ")]
        public string Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "confirm Password is Required ")]
        [Compare("Password", ErrorMessage = "Pasword Mismatch")]
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password ")]
        public string ConfirmPassword { get; set; }

        //  public string Approve { get; set; }
        [NotMapped]
        public bool isSelected { get; set; }
    
        public  List<UserOnEventActityDL> UserOnEventActities { get; set; }

        public List<PostCommentDL> PostComments { get; set; }
        public List<SharedContent> SharedContents { get; set; }
        public List<IdeaDL> Ideas { get; set; }
        public List<CommentOnIdeaDL> CommentOnIdeas { get; set; }
        public List<UserActivityOnIdea> UserActivityOnIdeas { get; set; }
        public List<ShareIdeaContentDL> ShareIdeaContents { get; set; }

        public List<ClubMemberDL> clubMembers { get; set; }

        public List<ServiceJoinerDL> ServiceJoiners { get; set; }


    }
}