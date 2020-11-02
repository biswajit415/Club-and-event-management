using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using University_Website.Models;

namespace University_Website.Controllers
{
    public class HomeController : Controller
    {
        UniversityContext context = new UniversityContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HomePage()
        {

            return View();

        }

        public ActionResult Notification()
        {
            int id = (int)Session["UserId"];
            var EventSharedList = context.SharedContents.Include("Events").Include("Users").Where(x => x.SharedWithUserID ==id ).ToList();
            var IdeaSharedList=context.ShareIdeaContents.Include("Ideas").Include("Users").Where(x => x.SharedWithUserID ==id).ToList();
            var EventStory = context.ShareEventStories.ToList();
            NotificationDL notification = new NotificationDL();
            notification.ShareEvents = EventSharedList;
            notification.ShareIdeas = IdeaSharedList;
            notification.ShareEventStories = EventStory;
            return View(notification);
        }
   
      


    }
}