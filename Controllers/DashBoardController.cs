using GrainMaster.Models;
using System;
using System.Web.Mvc;

namespace GrainMaster.Controllers
{
    public class DashBoardController : Controller
    {
        // GET: DashBoard
        public ActionResult Dashboard()
        {
            if (UserLogic.LoggedUser.Name == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View(PortFolio.GetByID());
        }

        [HttpGet]
        public ActionResult Settings()
        {
            if (UserLogic.LoggedUser.Name == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }
       
        [HttpPost]
        public ActionResult Settings(ChangePasswordModel changePasswordModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsersDetail usersDetail = UserLogic.AuthentiateUser(UserLogic.LoggedUser.Email, changePasswordModel.Password);
                    if (usersDetail.UserStatus == UserStatus.Active)
                    {
                        bool IsUpdate = Register.ChangePassword(changePasswordModel);
                        if (IsUpdate)
                            ViewData["result"] = "Password changed successfully !!";
                        RedirectToAction("Login", "Login");
                    }  
                    else if (usersDetail.UserStatus == UserStatus.NotFound)
                        ViewData["result"] = "Current password is not matching with system data!!";
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewData["result"] = ex.Message;
                return View();
            }
        }

        public ActionResult Users()
        {
            return View();
        }

        public ActionResult StaticNonAuth()
        {
            return View();
        }

        public ActionResult UserEdit()
        {
            return View();
        }
    }
}