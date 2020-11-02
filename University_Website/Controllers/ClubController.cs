using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using University_Website.Models;

namespace University_Website.Controllers
{
    public class ClubController : Controller
    {
        // GET: Club
        UniversityContext context = new UniversityContext();
        public ActionResult ClubList()
        {
            var ClubList = context.Clubs.ToList();
            ViewBag.ClubMsg = TempData["ClubMassage"];
            return View(ClubList);
        }

        public ActionResult ClubJoin(int ClubID)
        {
            TempData["ClubID"] = ClubID;
            string Dasignation = context.Clubs.Where(x => x.ClubID == ClubID).FirstOrDefault().Designations;
            List<string> DasignationList = Dasignation.Split(',').Select(p => p.Trim()).ToList();
            ViewBag.list = DasignationList;
            return View();
        }

        [HttpPost]
        public ActionResult ClubJoin(ClubMemberDL clubMember)
        {
            TempData.Keep("ClubID");
            var ClubID= (int)TempData["ClubID"];
            clubMember.ClubID = ClubID;
            string Dasignation = context.Clubs.Where(x => x.ClubID == ClubID).FirstOrDefault().Designations;
            List<string> DasignationList = Dasignation.Split(',').Select(p => p.Trim()).ToList();
            ViewBag.list = DasignationList;
            if (ModelState.IsValid)
            {
                var CurrentUserId = (int)Session["UserId"];
                int userId = clubMember.UserID;
                var clubId = (int)TempData["ClubID"];
                var dasignation = clubMember.Designation;
                var CheckOldMember = context.ClubMembers.Where(x => x.UserID == userId && x.ClubID==clubId).FirstOrDefault();
                var CheckDesignation = context.ClubMembers.Where(x => x.Designation == dasignation && x.ClubID == clubId).FirstOrDefault();

                if (CheckOldMember!=null && clubMember.UserID == CurrentUserId)
                {
                    ViewBag.ClubJoinMsg= "you are already member of this club";
                    return View();
                }
                if(CheckDesignation!=null && CheckOldMember==null)
                {
                    ViewBag.ClubJoinMsg = "Already present member with this designation";
                    return View();
                }
                if(clubMember.UserID!=CurrentUserId)
                {
                    ViewBag.ClubJoinMsg = "Please Check your user Id";
                    return View();
                }

                context.ClubMembers.Add(clubMember);
                context.SaveChanges();
                return RedirectToAction("ClubList");
            }
            return View(clubMember);
        }

        public ActionResult ClubLeave(int ClubID)
        {
            string ClubName = context.Clubs.Find(ClubID).ClubName;
            int userId = (int)Session["UserId"];
            TempData["ClubMassage"] = "You Have Left " + ClubName + " Club";
            ClubMemberDL LeaveUser = context.ClubMembers.Where(x => x.ClubID == ClubID && x.UserID == userId).FirstOrDefault();
            if (LeaveUser == null)
            {
                TempData["ClubMassage"] = "You are not member of " + ClubName + " Club, So you can not apply for leave";
                return RedirectToAction("ClubList");
            }

            context.ClubMembers.Remove(LeaveUser);
            context.SaveChanges();
            return RedirectToAction("ClubList");
        }

        public ActionResult ClubMembers(int ClubID)
        {
            var ListOfClubMembers = context.ClubMembers.Include("Users").Where(x => x.ClubID == ClubID).ToList();
            ViewBag.ClubMemberMsg = TempData["ClubMemberMsg"];
            return View(ListOfClubMembers);
        }

        public ActionResult ClubMemberRemove(int ClubID,int UserID)
        {
            var FindMember = context.ClubMembers.Include("Users").Where(x => x.ClubID == ClubID && x.UserID == UserID).FirstOrDefault();
       
            TempData["ClubMemberMsg"] = "Club Member" + FindMember.Users.FirstName + "has been removed";
            context.ClubMembers.Remove(FindMember);
            context.SaveChanges();
            return RedirectToAction("ClubMembers",new { ClubID= ClubID });
        }

        public ActionResult UpdateClubDetails(int ClubID)
        {
            var FindClub = context.Clubs.Where(x => x.ClubID == ClubID).FirstOrDefault();
            TempData["ClubID"] = ClubID;
            return View(FindClub);
        }

        [HttpPost]
        public ActionResult UpdateClubDetails(ClubDL club)
        {
            int ClubID = (int)TempData["ClubID"];
            TempData.Keep("ClubID");
            var FindClub = context.Clubs.Where(x => x.ClubID == ClubID).FirstOrDefault();
            if (ModelState.IsValid)
            {
           
                FindClub.ClubID = ClubID;
                FindClub.ClubDescription = club.ClubDescription;
                FindClub.Eligibility = club.Eligibility;
                FindClub.Designations = club.Designations;
                context.SaveChanges();
                return RedirectToAction("ClubList");
            }
            return View(FindClub);
            
        }

    }
}