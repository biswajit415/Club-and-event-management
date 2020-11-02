using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using University_Website.Models;

namespace University_Website.Controllers
{
    public class IdeaController : Controller
    {
        // GET: Idea
        UniversityContext context = new UniversityContext();
        UserActivityOnIdea uai = new UserActivityOnIdea();


        public ActionResult ViewAllIdea()
        {

            var FindAllIdea = context.Ideas.Include("Club").Include("Users").ToList();
            return View("ViewIdea",FindAllIdea);
        }


        public ActionResult ViewIdea()
        {
            int user = (int)Session["UserId"];
            var FindJoinedClubs = context.ClubMembers.Where(x => x.UserID == user).Select(x => x.ClubID).ToList();
            var findAllIdeasUnderClub = context.Ideas.Include("Club").Include("Users").Where(x => FindJoinedClubs.Contains(x.ClubID)).ToList();
            return View(findAllIdeasUnderClub) ;
        }

        public ActionResult IdeaDetails(int IdeaID)
        {
            var FindIdea = context.Ideas.Include("Users").Where(x => x.IdeaID ==IdeaID).FirstOrDefault();
            return View(FindIdea);


        }
   


        public ActionResult AddIdea()
        {
            var ClubNames = context.Clubs.Select(l => l.ClubName).ToList();
            ViewBag.Clubs = ClubNames;
         
            return View();
        }



        [HttpPost]
        public ActionResult AddIdea(IdeaDL idea)
        {
            var ClubNames = context.Clubs.Select(l => l.ClubName).ToList();
            ViewBag.Clubs = ClubNames;
            int user = (int)Session["UserId"];
            if (ModelState.IsValid)
            {
                idea.ClubID=context.Clubs.Where(x => x.ClubName == idea.ClubName).FirstOrDefault().ClubID;
                idea.UpVoteCount = 0;
                idea.DownVoteCount = 0;
                idea.PostIdeaDate = DateTime.Parse(DateTime.Today.ToString("yyyy-MM-dd H:mm:ss"));
                idea.UserID = user;
                idea.Approve = "Pending";
                context.Ideas.Add(idea);
                context.SaveChanges();
                
                return RedirectToAction("HomePage", "Home");
            }

            return View("AddIdea");

        }
        public ActionResult SerchByTitle(string search1)
        {
            var CurrentIdeaList = context.Ideas.Include("Users").ToList();

            if (search1 == null)
            {
                ViewBag.SearchByIdValidationMsg = "Id can not be null ";
                return View("ViewIdea", CurrentIdeaList);
            }

            var SearchIdea = context.Ideas.Include("Users").Where(x => x.IdeaTitle==search1).ToList();
            if (SearchIdea.Count == 0)
            {
                ViewBag.NotFoud = "Idea Not Found";
            }
            return View("ViewIdea", SearchIdea);
        }

        public ActionResult SerchByCategory(string search2)
        {
            var CurrentIdeaList = context.Ideas.Include("Users").ToList();

            if (search2 == null)
            {
                ViewBag.SearchByIdValidationMsg = "Id can not be null ";
                return View("ViewIdea", CurrentIdeaList);
            }

            var SearchIdea = context.Ideas.Include("Users").Where(x => x.Category == search2).ToList();
            if (SearchIdea.Count == 0)
            {
                ViewBag.NotFoud = "Idea Not Found";
            }
            return View("ViewIdea", SearchIdea);
        }
        public ActionResult Comment(int IdeaID)
        {
            var CommentList = context.CommentOnIdeas.Where(x => x.IdeaID == IdeaID).ToList();
            var Idea = context.Ideas.Where(x => x.IdeaID == IdeaID).FirstOrDefault();
            IdeaComment ic = new IdeaComment();
            ic.Idea = Idea;
            ic.CommentOnIdeas= CommentList;
            ic.IdeaID = IdeaID;
            return View("Comment", ic);
        }

        [HttpPost]
        public ActionResult AddComment(IdeaComment ic)
        {
            int user = (int)Session["UserId"];
            CommentOnIdeaDL coi = new CommentOnIdeaDL();
            int id = ic.IdeaID;
            coi.UserID = user;
            coi.IdeaID = ic.IdeaID;
            coi.Comment = ic.Comment;
            coi.Name = context.Users.Where(x => x.UserID == user).FirstOrDefault().FirstName;

            context.CommentOnIdeas.Add(coi);
            context.SaveChanges();
            return RedirectToAction("Comment", new { IdeaID = id });

        }




        public ActionResult UpVote(int IdeaID)
        {
            int user = (int)Session["UserId"];
            var SearchUser = context.UserActivityOnIdeas.Include("Ideas").Where(x => x.UserID == user && x.IdeaID == IdeaID).FirstOrDefault();
            var SearchIdea = context.Ideas.Where(x => x.IdeaID == IdeaID).FirstOrDefault();
            uai.UpVote = 1;
            uai.DownVote = 0;
            uai.IdeaID = IdeaID;
            uai.UserID = user;
         

             if (SearchUser == null)
            {
                SearchIdea.UpVoteCount += 1;
                context.SaveChanges();
                context.UserActivityOnIdeas.Add(uai);
                context.SaveChanges();
            }
            if (SearchUser != null)
            {
                if (SearchUser.UpVote == 0)
                {

                    SearchIdea.UpVoteCount = SearchIdea.UpVoteCount + 1;
                    SearchIdea.DownVoteCount = SearchIdea.DownVoteCount - 1;
                }

                SearchUser.UpVote = 1;
                SearchUser.DownVote = 0;
              
                context.SaveChanges();
            }

            return RedirectToAction("ViewIdea");


        }

        public ActionResult DownVote(int IdeaID)
        {
            int user = (int)Session["UserId"];
            var SearchUser = context.UserActivityOnIdeas.Where(x => x.UserID == user && x.IdeaID == IdeaID).FirstOrDefault();
            var SearchIdea = context.Ideas.Where(x => x.IdeaID == IdeaID).FirstOrDefault();
            uai.UpVote = 0;
            uai.DownVote = 1;
            uai.IdeaID = IdeaID;
            uai.UserID = user;

            if (SearchUser == null)
            {
                SearchIdea.DownVoteCount = SearchIdea.DownVoteCount + 1;
             
                context.UserActivityOnIdeas.Add(uai);
                context.SaveChanges();
            }
            if (SearchUser != null)
            {
                if (SearchUser.DownVote == 0)
                {
                    SearchIdea.UpVoteCount = SearchIdea.UpVoteCount - 1;
                    SearchIdea.DownVoteCount = SearchIdea.DownVoteCount + 1;
                }
             
                SearchUser.UpVote = 0;
                SearchUser.DownVote = 1;
                context.SaveChanges();

            }

            return RedirectToAction("ViewIdea");


        }

        public ActionResult IdeaShare(int IdeaID)
        {
            TempData["IdeaID"]=IdeaID;
      
            return View("IdeaShare");

        }
        
        [HttpPost]
        public ActionResult IdeaShare(ShareFormDL shareForm)
        {
            ShareIdeaContentDL sc = new ShareIdeaContentDL();
            var FindUser = context.Users.Where(x => x.PhoneNumber == shareForm.PhoneNumber &&
              x.UserID == shareForm.UserID
            ).FirstOrDefault();

            if (ModelState.IsValid && FindUser!=null)
            {
                sc.IdeaID = (int)TempData["IdeaID"];
                sc.UserID = (int)Session["UserId"];
                sc.SharedWithUserID = shareForm.UserID;
                context.ShareIdeaContents.Add(sc);
                context.SaveChanges();

                return RedirectToAction("ViewIdea");
            }
            if (FindUser == null && ModelState.IsValid)
            {
                ViewBag.ValidationError = "You Entered wrong user id or password";

            }
            return View("IdeaShare");

        }

        public ActionResult IdeaReject(int IdeaID)
        {
            var FindIdea=context.Ideas.Where(x => x.IdeaID == IdeaID).FirstOrDefault();
            FindIdea.Approve = "Rejected";
            context.SaveChanges();
            return RedirectToAction("ViewAllIdea");

        }
        public ActionResult IdeaAccepet(int IdeaID)
        {
            var FindIdea = context.Ideas.Where(x => x.IdeaID == IdeaID).FirstOrDefault();
            FindIdea.Approve = "Accepted";
            context.SaveChanges();
            return RedirectToAction("ViewAllIdea");
        }
             
       

    }
}