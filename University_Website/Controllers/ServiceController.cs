using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using University_Website.Models;

namespace University_Website.Controllers
{
    public class ServiceController : Controller
    {
        UniversityContext context = new UniversityContext();
        // GET: Service
       public ActionResult ServiceList()
       {
            ViewBag.ServiceMsg = TempData["ServiceMsg"];
            var ServiceList = context.Services.ToList();
            return View(ServiceList);
       }

        public ActionResult ServiceJoin(int ServiceID,string Slot)
        {
            List<string> SlotList = Slot.Split(',').Select(p => p.Trim()).ToList();
            int UserId = (int)Session["UserId"];
            DateTime today = DateTime.Parse(DateTime.Today.ToString("yyyy-MM-dd H:mm:ss"));
            int CountVolunteer = context.ServiceJoiners.Where(x => x.ServiceID == ServiceID).ToList().Count;
            int RequiredVolunteer = context.Services.Find(ServiceID).Volunteer;
            int TotalVlunteerCount = (SlotList.Count)*RequiredVolunteer;
            var FindUser = context.ServiceJoiners.Where(x=>x.ServiceID==ServiceID && x.UserID==UserId).FirstOrDefault();
            ViewBag.ValidationErr = TempData["ValidationErr"];
            if (FindUser != null)
            {
                TempData["ServiceMsg"] = "You have already joined in this Service plan";
                return RedirectToAction("ServiceList");
            }
            else if (CountVolunteer == TotalVlunteerCount)
            {
                TempData["ServiceMsg"] = "Sorry Max volunteers nominated";
                return RedirectToAction("ServiceList");
            }
            else
            {
                TempData["ServiceID"] = ServiceID;
                TempData["SlotList"] = Slot;
                List<string> AvailableSlots = new List<string>();
                foreach(var dateSlot in SlotList)
                {

                    int SlotWithVolunteerCount = context.ServiceJoiners.Where(x => x.Slot == dateSlot && x.ServiceID==ServiceID ).ToList().Count;
                    if (RequiredVolunteer != SlotWithVolunteerCount && DateTime.Parse(dateSlot)>=today)
                        AvailableSlots.Add(dateSlot);
                }
                
                ViewBag.SlotList = AvailableSlots;
                TempData["ListData"] = AvailableSlots;
                return View();

            }



        }

        [HttpPost]
        public ActionResult ServiceJoin(ServiceJoinForm sf)
        {
            int UserID = sf.UserID;
            long PhnNum = sf.PhoneNumber;
            string slot = sf.Slot;
            int ServiceID= (int)TempData["ServiceID"];
            string SlotList = (string)TempData["SlotList"];
            TempData.Keep("ListData");
            TempData.Keep("ServiceID");
            TempData.Keep("SlotList");
            if (ModelState.IsValid)
            {  
                var FindUser = context.Users.Where(x => x.UserID == UserID && x.PhoneNumber == PhnNum).FirstOrDefault();
          
                if (FindUser == null)
                {
                    TempData["ValidationErr"] = "Invalid User Id or phone Number";
                    return RedirectToAction("ServiceJoin", new RouteValueDictionary(new { ServiceID=ServiceID, Slot = SlotList }));
                }

                ServiceJoinerDL sj = new ServiceJoinerDL();
                 sj.UserID = UserID;
                 sj.ServiceID = ServiceID;
                 sj.Slot = slot;
                context.ServiceJoiners.Add(sj);
                 context.SaveChanges();
               
                return RedirectToAction("ServiceList");
            }
            ViewBag.SlotList =(List<string>)TempData["ListData"];
 
            return View(sf);

        }

        public ActionResult ServiceLeave(int ServiceID)
        {
            int UserId = (int)Session["UserId"];
            var LeaveMember = context.ServiceJoiners.Where(x => x.UserID == UserId && x.ServiceID == ServiceID).FirstOrDefault();

            if (LeaveMember == null)
            {
                TempData["ServiceMsg"] = "You Have Not Joined in this Service";
                return RedirectToAction("ServiceList");
            }
            TempData["ServiceMsg"] = "You Have Left this Service";
            context.ServiceJoiners.Remove(LeaveMember);
            context.SaveChanges();
            return RedirectToAction("ServiceList");

        }

        public ActionResult ServiceMembers(int ServiceID)
        {
            var MemberList = context.ServiceJoiners.Include("Users").Where(x => x.ServiceID ==ServiceID).ToList();


            if(MemberList.Count==0)
            {
               
               TempData["ServiceMsg"] = "Till now no members joined this service";
                return RedirectToAction("ServiceList");
            }
            
            return View(MemberList);

        }

        public ActionResult CreateService()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateService(ServiceDL service)
        {
            if (ModelState.IsValid)
            {
                List<string> SlotList = service.Slots.Split(',').Select(p => p.Trim()).ToList();
                DateTime dateValue;
                DateTime today = DateTime.Parse(DateTime.Today.ToString("yyyy-MM-dd H:mm:ss"));

                foreach (string dateString in SlotList)
                {

                    if (!DateTime.TryParseExact(dateString, "yyyy-MM-dd",CultureInfo.InvariantCulture,DateTimeStyles.None, out dateValue))
                    {
                        
                        ViewBag.DateErrorMSG= "  Unable to recognize "+dateString+" in slots . Please follow YYYY-MM-dd format";
                        return View(service);
                    }
                    DateTime SlotDate = DateTime.Parse(dateString);
                    if (SlotDate < today)
                    {
                        ViewBag.DateErrorMSG = " Slot Date" + dateString + " smaller than todays date";
                        return View(service);
                    }

                }
                context.Services.Add(service);
                context.SaveChanges();
               
                return RedirectToAction("ServiceList");
            }

            return View(service);
        }

        public ActionResult UpdateService(int ServiceID)
        {
            var CurrentDate = DateTime.Parse(DateTime.Today.ToString("yyyy-MM-dd"));

            var FindService = context.Services.Where(x => x.ServiceID == ServiceID).FirstOrDefault();
            string slot = FindService.Slots;
            List<string> SlotList = slot.Split(',').Select(p => p.Trim()).ToList();
           
             foreach(var i in SlotList)
            {
                DateTime SlotDate= DateTime.Parse(i);
                if (SlotDate < CurrentDate)
                {
                    TempData["ServiceMsg"] = "Your Slot Dates are smaller than current date";
                    return RedirectToAction("ServiceList");
                }
            }




            return View(FindService);
        }

        [HttpPost]
        public ActionResult UpdateService(ServiceDL service)
        {
            if (ModelState.IsValid)
            {
                var FindService = context.Services.Where(p => p.ServiceID == service.ServiceID).FirstOrDefault();
                FindService.ServiceName = service.ServiceName;
                FindService.ServiceDescription = service.ServiceDescription;
                FindService.Volunteer = service.Volunteer;
                context.SaveChanges();
                return RedirectToAction("ServiceList");
            }
            return View();
        }

   

    }
}