using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace University_Website.Models
{
    [Table("ShareIdeaContentDL")]
    public class ShareIdeaContentDL
    {
        [Key]
        public int ShareIdeaContentID { get; set; }

        public int IdeaID { get; set; }
        
        public int SharedWithUserID { get; set; }
        public int UserID { get; set; }

        public IdeaDL Ideas { get; set; }
        public UserDL Users { get; set; }

    }
}