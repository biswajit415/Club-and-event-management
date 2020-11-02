using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace University_Website.Models
{
    public class CommentOnIdeaDL
    {   

        [Key]
        public int CommentId { get; set; }
        public string Comment { get; set; }

        public string Name { get; set; }
        public int IdeaID { get; set; }

        public int UserID { get; set; }

        public UserDL Users { get; set; }
        public IdeaDL Ideas { get; set; }
    }
}