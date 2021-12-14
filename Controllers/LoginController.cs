using System;
using System.Web.Mvc;
using GrainMaster.Models;

namespace GrainMaster.Controllers
{
    
    public class LoginController : Controller
    {
        // GET: Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(FormCollection formCollection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = new User()
                    {
                        Email = Convert.ToString(formCollection["Email"]),
                        Password = Convert.ToString(formCollection["Password"])
                    };
                    UsersDetail usersDetail =  UserLogic.AuthentiateUser(user.Email, user.Password);
                    if (usersDetail.UserStatus == UserStatus.Active)
                        return RedirectToAction("Dashboard", "DashBoard");
                    else if(usersDetail.UserStatus == UserStatus.Inactive)
                        TempData["msg"] = "User is not active !!";
                    else if (usersDetail.UserStatus == UserStatus.NotFound)
                        TempData["msg"] = "Invalid userid/password!!";
                }
                return View();
            }
            catch(Exception ex)
            {
                TempData["msg"] = ex.Message;
                return View();
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult VerifyEmail()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult ResetPassword()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ResetPassword(FormCollection formCollection)
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session["LoggedUser"] = null;
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }

    }
}