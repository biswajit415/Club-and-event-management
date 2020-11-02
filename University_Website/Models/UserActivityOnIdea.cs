using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace University_Website.Models
{
    [Table("UserActivityOnIdeaTbl")]
    public class UserActivityOnIdea
    {   
        [Key]
        public int UserActivityOnIdeaID { get; set; }

        
       
        public int UpVote { get; set; }

        public int DownVote { get; set; }

       
        public int IdeaID { get; set; }

        public IdeaDL Ideas { get; set; }

        public int UserID { get; set; }
        public UserDL Users { get; set; }
        
    }
}