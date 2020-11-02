using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace University_Website.Models
{
    public class NotificationDL
    {
        public List<SharedContent> ShareEvents { get; set; }

        public List<ShareIdeaContentDL> ShareIdeas { get; set; }

        public List<ShareEventStoryDL> ShareEventStories { get; set; }

    }
}