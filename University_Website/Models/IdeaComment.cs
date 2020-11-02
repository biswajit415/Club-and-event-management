using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace University_Website.Models
{
    public class IdeaComment
    {
        public IdeaDL Idea { get; set; }
        public string Comment { get; set; }
        public int IdeaID{ get; set; }

        public List<CommentOnIdeaDL> CommentOnIdeas;
    }
}