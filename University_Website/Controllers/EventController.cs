using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using University_Website.Models;

namespace University_Website.Controllers
{
    public class EventController : Controller
    {
        // GET: Event
        UniversityContext context = new UniversityContext();
        UserOnEventActityDL activity=new UserOnEventActityDL();
    
      
        public ActionResult Event()
        {
            DateTime today = DateTime.Parse(DateTime.Today.ToString("yyyy-MM-dd H:mm:ss"));
            var CurrentEventList = context.Events.Where(x=>x.StartDate>=today).OrderByDescending(s => s.StartDate).ToList();
            ViewBag.Massege = TempData["UserActivityMsg"];
            ViewBag.ErrMsg = TempData["UserActivityErrorMsg"];
            return View(CurrentEventList);
        }

        public ActionResult EventDetails(int EventID)
        {
            var FindEvent = context.Events.Where(x => x.EventID == EventID).FirstOrDefault();
            return View(FindEvent);


        }

   
        public ActionResult SerchById(int? search)
        {
            var CurrentEventList = context.Events.ToList();
      
            if (search == null)
            {
                ViewBag.SearchByIdValidationMsg = "Id can not be null ";
                return View("Event", CurrentEventList);
            }

           var SearchEvent= context.Events.Where(x => x.EventID == search).ToList();
            if (SearchEvent.Count == 0)
            {
                ViewBag.NotFoud = "Event Not Found";
            }


           return View("Event", SearchEvent);
        }
     
        public ActionResult SerchByDate(DateTime? startDate, DateTime? endDate )
        {

       
            

            var CurrentEventList = context.Events.ToList();
            if (startDate > endDate)
            {
                ViewBag.SearchValidationMsg = "Start Date Can Not be Greater Than End Date ";
                return View("Event", CurrentEventList);
            }
            else if (startDate == null)
            {
                ViewBag.SearchValidationMsg = "Start Date Cannot Be Empty";
                return View("Event", CurrentEventList);
            }
            else if (endDate == null)
            {
                ViewBag.SearchValidationMsg = "Start Date Cannot Be Empty";
                return View("Event", CurrentEventList);
            }
            else
            {
                startDate = DateTime.Parse(startDate?.ToString("yyyy-MM-dd H:mm:ss"));
                endDate = DateTime.Parse(endDate?.ToString("yyyy-MM-dd H:mm:ss"));
                var SearchEvent = context.Events.Where(x => x.StartDate >= startDate && x.EndDate <= endDate).ToList();
                if (SearchEvent.Count == 0)
                {
                    ViewBag.NotFoud = "Event Not Found";
                }
                return View("Event", SearchEvent);
            }
        }

   
        

        public ActionResult Like(int EventID)
        {
            UserOnEventActityDL activity = new UserOnEventActityDL();
            int user=(int)Session["UserId"];
            var SearchEvent = context.Events.Where(x => x.EventID == EventID).FirstOrDefault();
            var SearchUser = context.UserOnEventActities.Where(x => x.UserID ==user && x.EventID==EventID).FirstOrDefault();
            activity.Attendence = "NA";
            activity.EventID = EventID;
            activity.UserID = user;
            activity.Like = 1;
            activity.Dislike = 0;
            activity.Interested = "NA";
            activity.NotInterested = "NA";
 
            if (SearchUser == null)
            {
                SearchEvent.LikeCount += 1;
                context.UserOnEventActities.Add(activity);
                context.SaveChanges();
            }
            if (SearchUser != null)
            {
                if(SearchUser.Like==0  )
                {
                    SearchEvent.LikeCount += 1;
                    if(SearchUser.Dislike==1)
                    SearchEvent.DisLikeCount -= 1;
                }

                SearchUser.Like = 1;
                SearchUser.Dislike = 0;
                context.SaveChanges();
            }
      
            return RedirectToAction("Event");
        }

        public ActionResult DisLike(int EventID)
        {
            int user = (int)Session["UserId"];
            var SearchUser = context.UserOnEventActities.Where(x => x.UserID == user && x.EventID == EventID).FirstOrDefault();
            var SearchEvent = context.Events.Where(x => x.EventID == EventID).FirstOrDefault();


            UserOnEventActityDL activity = new UserOnEventActityDL();

            if (SearchUser == null)
          {
                activity.Attendence = "NA";
                activity.EventID = EventID;
                activity.UserID = user;
                activity.Like = 0;
                activity.Dislike = 1;
                activity.Interested = "NA";
                activity.NotInterested = "NA";
                context.UserOnEventActities.Add(activity);
                context.SaveChanges();
            }
            if (SearchUser != null)
            {
                if (SearchUser.Dislike == 0)
                {
                    SearchEvent.DisLikeCount += 1;
                    if (SearchUser.Like == 1)
                        SearchEvent.LikeCount -= 1;
                
                      
                }
        
                SearchUser.Like = 0;
                SearchUser.Dislike = 1;
                context.SaveChanges();
            }
          
           
            return RedirectToAction("Event");
        }

        public ActionResult Attendence(int EventID)
        {
            int user = (int)Session["UserId"];
          
            var SearchUser = context.UserOnEventActities.Where(x => x.UserID == user && x.EventID == EventID).FirstOrDefault();

            DateTime FindDate = context.Events.Where(x => x.EventID == EventID).FirstOrDefault().StartDate;
            DateTime today = DateTime.Parse(DateTime.Today.ToString("yyyy-MM-dd H:mm:ss"));

         

            if (SearchUser == null)
            {
                activity.Attendence = "Present";
                activity.EventID = EventID;
                activity.UserID = user;
                activity.Like = 0;
                activity.Dislike = 0;
                activity.Interested = "NA";
                activity.NotInterested = "NA";

                if (FindDate > today)
                {
                    TempData["UserActivityErrorMsg"] = "Sorry You cant put attendence before event date";
                    return RedirectToAction("Event");
                }
   
                

            }
            if (SearchUser != null)
            {

                if (FindDate > today)
                {
                    TempData["UserActivityErrorMsg"] = "Sorry You cant put attendence before event date";
                    return RedirectToAction("Event");
                }

                if (FindDate < today && SearchUser.NotInterested=="Yes" )
                {
                    TempData["UserActivityErrorMsg"] = "    You Are not going to attend in this event ";
                    return RedirectToAction("Event");
                }
                if (FindDate < today && SearchUser.Interested == "NA")
                {
                    TempData["UserActivityErrorMsg"] = " You have not givven any interested reaction ";
                    return RedirectToAction("Event");
                }
                    SearchUser.Attendence = "Present";
                context.UserOnEventActities.Add(activity);
                context.SaveChanges();

             
            }

            TempData["UserActivityMsg"] = "You Have Marked Your Attendence";

            return RedirectToAction("Event");

        }
        public ActionResult Interest(int EventID)
        {
            int user = (int)Session["UserId"];
            DateTime today = DateTime.Parse(DateTime.Today.ToString("yyyy-MM-dd H:mm:ss"));
            var FindEvent = context.Events.Where(x => x.EventID == EventID).FirstOrDefault();
            if(FindEvent.EndDate<today)
            {
                TempData["UserActivityErrorMsg"] = " Event Already Finished";
                return RedirectToAction("Event");
            }
            

            var SearchUser = context.UserOnEventActities.Where(x => x.UserID == user && x.EventID == EventID).FirstOrDefault();
            if (SearchUser == null)
            {
                activity.Attendence = "Present";
                activity.EventID = EventID;
                activity.UserID = user;
                activity.Like = 0;
                activity.Dislike = 0;
                activity.Interested = "Yes";
                activity.NotInterested = "No";
                context.UserOnEventActities.Add(activity);
                context.SaveChanges();
                TempData["UserActivityMsg"] = "You Showed Your Interest";
                return RedirectToAction("Event");
            }

            if (SearchUser != null && SearchUser.NotInterested == "Yes")
            {
                TempData["UserActivityMsg"] = "You already not interested to this event";
                return RedirectToAction("Event");
            }
            if (SearchUser != null && SearchUser.Interested == "NA" && SearchUser.NotInterested == "NA")
            {
                SearchUser.Interested = "Yes";
                SearchUser.NotInterested = "No";
                context.SaveChanges();
                TempData["UserActivityMsg"] = "You Showed Your Interest";
            }


            TempData["UserActivityMsg"] = "You  Showed Your Interest";
            return RedirectToAction("Event");

        }
        public ActionResult NotInterest(int EventID)
        {
            int user = (int)Session["UserId"];
            DateTime today = DateTime.Parse(DateTime.Today.ToString("yyyy-MM-dd H:mm:ss"));
            var FindEvent = context.Events.Where(x => x.EventID == EventID).FirstOrDefault();
            if (FindEvent.EndDate < today)
            {
                TempData["UserActivityErrorMsg"] = " Event Already Finished ";
                return RedirectToAction("Event");
            }
            var SearchUser = context.UserOnEventActities.Where(x => x.UserID == user && x.EventID == EventID).FirstOrDefault();
            if (SearchUser == null)
            {
                activity.Attendence = "Present";
                activity.EventID = EventID;
                activity.UserID = user;
                activity.Like = 0;
                activity.Dislike = 0;
                activity.Interested = "No";
                activity.NotInterested = "Yes";
                context.UserOnEventActities.Add(activity);
                TempData["UserActivityMsg"] = "You are not Interested in this event";
                context.SaveChanges();
            }
            if (SearchUser != null && SearchUser.Interested=="Yes")
            {
                TempData["UserActivityMsg"] = "You Have Already Shown Interest";
                return RedirectToAction("Event");
            }
            if (SearchUser != null && SearchUser.Interested == "NA" && SearchUser.NotInterested == "NA")
            {
                SearchUser.Interested = "No";
                SearchUser.NotInterested = "Yes";
                context.SaveChanges();
                TempData["UserActivityMsg"] = "You are not Interested in this event";
            }
            TempData["UserActivityMsg"] = "You are not Interested in this event";
            return RedirectToAction("Event");

        }

        public ActionResult Comment(int EventID)
        {
            var CommentList=context.PostComments.Where(x=>x.EventID==EventID).ToList();
            var Event = context.Events.Where(x => x.EventID == EventID).FirstOrDefault();
            EventComment ec = new EventComment();
            ec.Event = Event;
            ec.PostComments = CommentList;
            ec.EventID = EventID;
            
            return View("Comment",ec);
        }

        [HttpPost]
        public ActionResult AddComment (EventComment EC)
        {
            int user = (int)Session["UserId"];
            PostCommentDL PC = new PostCommentDL();
            int id = EC.EventID;
           PC.UserID = user;
            PC.EventID = EC.EventID; 
           PC.Comment = EC.Comment;
           PC.Name = context.Users.Where(x => x.UserID == user).FirstOrDefault().FirstName;

             context.PostComments.Add(PC);
            context.SaveChanges();
            return RedirectToAction("Comment",new { EventID =id});

        }
        public ActionResult EventShare(int EventID)
        {
            TempData["EventID"] = EventID;

            return View("EventShare");

        }

        [HttpPost]
        public ActionResult EventShare(ShareFormDL shareForm)
        {
            SharedContent sc = new SharedContent();
            var FindUser = context.Users.Where(x => x.PhoneNumber == shareForm.PhoneNumber &&
              x.UserID == shareForm.UserID
            ).FirstOrDefault();

            if (ModelState.IsValid && FindUser != null)
            {
                sc.EventID = (int)TempData["EventID"];
                sc.UserID = (int)Session["UserId"];
                sc.SharedWithUserID = shareForm.UserID;
                context.SharedContents.Add(sc);
                context.SaveChanges();

                return RedirectToAction("Event");
            }
            if (ModelState.IsValid && FindUser == null)
            {
                ViewBag.ValidationError = "You Entered wrong user id or password";

            }
            return View("EventShare");

        }




        public ActionResult Attenders(int EventID)
        {
            var attenderList = context.UserOnEventActities.Include("Users").Where(x => x.Interested == "Yes" && x.EventID == EventID);
            return View(attenderList);


        }

        public ActionResult CreateEvent()
        {
            var ClubNames = context.Clubs.Select(l => l.ClubName).ToList();
            ViewBag.Clubs = ClubNames;
            return View();
        }

        [HttpPost]
        public ActionResult CreateEvent(EventDL Event)
        {
            var ClubNames = context.Clubs.Select(l => l.ClubName).ToList();
            ViewBag.Clubs = ClubNames;
            DateTime today = DateTime.Parse(DateTime.Today.ToString("yyyy-MM-dd H:mm:ss"));
            if (ModelState.IsValid)
            {
                if (Event.StartDate > Event.EndDate)
                {
                    ViewBag.errMsg = "Start Date Should Be Less Than End Date";
                    return View();
                }
                if(Event.StartDate < today)
                {
                    ViewBag.errMsg = "Start Date can not Be Less Than Current Date";
                    return View();
                }
                Event.LikeCount = 0;
                Event.DisLikeCount = 0;
                context.Events.Add(Event);
                context.SaveChanges();
                return RedirectToAction("Event");
            }
          
            return View();
         
        }

        public ActionResult DeleteEvent(int EventID)
        {

            var parent = context.Events.Include("UserOnEventActitys")
                  .Include("PostComments")
                  .Include("SharedContents")
                  .Where(x => x.EventID == EventID)
                  .SingleOrDefault(p => p.EventID == p.EventID);

            foreach (var child in parent.UserOnEventActitys.ToList())
                context.UserOnEventActities.Remove(child);
            foreach (var child in parent.PostComments.ToList())
                context.PostComments.Remove(child);
            foreach (var child in parent.SharedContents.ToList())
                context.SharedContents.Remove(child);
            context.Events.Remove(parent);
            context.SaveChanges();
            return RedirectToAction("Event");
        }

        public ActionResult ShareEventStory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ShareEventStory(ShareEventStoryDL shareEventStory)
        {
            if (ModelState.IsValid)
            {
                context.ShareEventStories.Add(shareEventStory);
                context.SaveChanges();
                return RedirectToAction("HomePage", "Home");
            }

            return View(shareEventStory);
        }

        public ActionResult StoryDetails(int ShareEventStoryID)
        {
            var FindEventStory = context.ShareEventStories.Where(x => x.ShareEventStoryID == ShareEventStoryID).FirstOrDefault();
            return View(FindEventStory);
        }



    }
}