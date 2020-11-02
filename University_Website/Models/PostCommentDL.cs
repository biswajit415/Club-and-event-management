using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace University_Website.Models
{
    [Table("PostCommentTbl")]
    public class PostCommentDL
    {
        [Key]    
        public int CommentId { get; set; }
        public string Comment { get; set; }

        public string Name { get; set; } 
        public int EventID {get; set;}
        
        public int UserID { get; set; }

        public UserDL users { get; set; }
        public EventDL Events { get; set; }

    }
}