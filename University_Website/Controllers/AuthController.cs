using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using University_Website.Models;

namespace University_Website.Controllers
{


    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult login(string UserRole)

        {


            if (UserRole == "Admin")
            {
                return RedirectToAction("AdminLogin");
            }
            else if (UserRole == "User")
            {
                return RedirectToAction("UserLogin");
            }
            else
            {
                ViewBag.Message = "** Please Choose Your Role **";
            }
            return View();
        }

        public ActionResult AdminLogin()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminLogin(AdminDL adminDL)
        {
            ViewBag.Name = "Admin";
   UniversityContext context = new UniversityContext();
            if (ModelState.IsValid)
            {
                var searchAdmin = context.Admins.Where(x => x.UserId == adminDL.UserId &&
                  x.Password == adminDL.Password).FirstOrDefault();
                if (searchAdmin != null) //ie login successful
                {
                    ViewBag.isValidate = 1;
                    Session["AdminId"] = adminDL.UserId;
                    Session["Name"] = "Admin";
                    return RedirectToAction("HomePage", "Home");
                }
                else
                {
                    ViewBag.isValidate = 0;
                    ViewBag.ValidationMsg = "Invalid Admin ID or Incorrect Password";
                }
            }
            return View(adminDL);
        }
        public ActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserLogin(UserDL userDl)
        {
            UniversityContext context = new UniversityContext();
            
            if (userDl.Password == null)
            {
                ViewBag.PasswordValidation = "Password is required";
                return View(userDl);
            }
            var searchUser = context.Users.Where(x => x.UserID == userDl.UserID &&
              x.Password == userDl.Password).FirstOrDefault();

            if (searchUser == null) //if username and password is matching
            {
                ViewBag.isValidate = 0;
                ViewBag.ValidationMsg = " Invalid User ID or Incorrect Password ";
                return View(userDl);
            }
            else
            {
                Session["Name"] = context.Users.Where(x => x.UserID == userDl.UserID).FirstOrDefault().FirstName;
                ViewBag.isValidate = 1;
                ViewBag.ValidationMsg = " Logged in successfully ";
                Session["UserId"] = userDl.UserID;
                return RedirectToAction("HomePage", "Home");
            }


        }


        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(UserDL user)
        {
            UniversityContext context = new UniversityContext();
            var ExistUser = context.Users.Where(x => x.UserID == user.UserID).FirstOrDefault();

            if (ModelState.IsValid)
            {
                if (ExistUser != null)
                {
                    ViewBag.newUserValidate = 0;
                    ViewBag.newUserValidationMsg = "User Already Registered ";
                }

                else
                {

                    context.Users.Add(user);
                    context.SaveChanges();
                    ViewBag.newUserValidate = 1;
                    ViewBag.newUserValidationMsg = "Your details are submitted successfully";
                    return View(user);
                }
            }

            return View(user);
        }



        /*public ActionResult Profile()
        {

        }*/

        public ActionResult Logout()
        {
            Session["UserId"]=null;
            Session["AdminId"] = null;
            Session["Name"] = null;
            return RedirectToAction("login", "Auth");
        }
      
   
        
         
     

    }
}