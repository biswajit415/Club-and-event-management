using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace University_Website.Models
{
    public class EventComment
    {
        public EventDL Event { get; set; }
        public string Comment { get; set; }
        public int EventID { get; set; }
       
        public List<PostCommentDL> PostComments;
    }
}